using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Whose_Turn.Context.Entities;
using System.Threading.Tasks;
using Whose_Turn.Models;
using Whose_Turn.Context;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Whose_Turn.Services;
using Whose_Turn.ConfigModels;
using System.Security.Claims;
using System.Collections.Generic;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Whose_Turn.Models.Account;

namespace Whose_Turn.Managers
{
    public class AuthResult
    {
        /// <summary>
        /// Gets or sets the resykt
        /// </summary>
        public SignInResult Result { get; set; }

        /// <summary>
        /// Gets or sets the token if <see cref="Result"/> is Succeeded
        /// </summary>
        public string Token { get; set; }
    }

    public class WhoseTurnUserManager : IDisposable
    {
        private static class LogEvents
        {
            public static readonly EventId CreatingUser = new EventId(1, nameof(CreateUser));
        }

        /// <summary>
        /// Gets the type of the security stamp claim.
        /// </summary>
        /// <value>The type of the security stamp claim.</value>
        public string SecurityStampClaimType
            => "WHOSE_TURN.Identity.SecurityStamp";

        private readonly ILogger _logger;

        private readonly PasswordHashing _passwordHasher;
        private readonly DatabaseContext _databaseContext;

        private readonly JwtTokenConfig _jwtTokenConfig;

        public WhoseTurnUserManager(IServiceProvider serviceProvider)
        {
            _logger = serviceProvider.GetService<ILogger<WhoseTurnUserManager>>();
            _passwordHasher = serviceProvider.GetService<PasswordHashing>();
            _databaseContext = serviceProvider.GetService<DatabaseContext>();

            _jwtTokenConfig = serviceProvider.GetService<JwtTokenConfig>();
        }

        /// <summary>
        /// Retrieves the user with the the given id
        /// </summary>
        /// <param name="id"> The user identifier</param>
        /// <returns></returns>
        public  Task<User> FindUserById(Guid id)
        {
            var user = _databaseContext.Users.FindAsync(id).AsTask();
            return user;
        }

        /// <summary>
        /// Retrieves the user with the the given id
        /// </summary>
        /// <param name="id"> The user identifier</param>
        /// <returns></returns>
        public Task<User> FindUserByEmail(string email)
        {
            var user = _databaseContext.Users
                .Where(u => u.Email == email)
                .FirstOrDefaultAsync();

            return user;
        }




        /// <summary>
        /// Will try to authenticate using the provided details and return an access token token if successfull.
        /// </summary>
        /// <param name="model"></param>
        /// <returns> Access token if successful login </returns>
        public async Task<AuthResult> SignIn(LoginModel model)
        {
            if (string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Password))
                throw new ArgumentNullException($"{nameof(model.Email)} or {nameof(model.Password)}");

            var user = await _databaseContext.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == model.Email.ToLower());

            if (user == null)
            {
                return new AuthResult
                {
                    Result = SignInResult.Failed
                };
            }

            var precheckResult = PreLoginChecks(user);
            if (precheckResult != SignInResult.Success)
            {
                return new AuthResult
                {
                    Result = precheckResult
                };
            }

            if (!ValidatePassword(user, model.Password))
            {
                return new AuthResult
                {
                    Result = SignInResult.Failed
                };
            }

            if (NeedsTwoFactor(user))
            {
                return new AuthResult
                {
                    Result = SignInResult.TwoFactorRequired
                };
            }

            return new AuthResult
            {
                Result = SignInResult.Success,
                Token = await GenerateTokenAsync(user)
            };
        }

        /// <summary>
        /// Creates a new user using the given model
        /// </summary>
        /// <returns></returns>
        public async Task<AuthResult> CreateUser(CreateUserModel model)
        {
            _logger.LogInformation(LogEvents.CreatingUser, "Creating user with email {emailAddress}", model.EmailAddress);

            if (_databaseContext.Users.Any(u => u.Email == model.EmailAddress))
            {
                _logger.LogWarning(LogEvents.CreatingUser, "Could not create user with email {emailAddress}, a user with the given email exists",
                    model.EmailAddress);

                return new AuthResult
                {
                    Result = SignInResult.Failed,
                    Token = "Email address is arlready taken"
                };
            }

            var user = new User
            {
                Id = Guid.NewGuid(),
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.EmailAddress,
                Hash = _passwordHasher.HashPassword(model.Password),
                CreatedOn = DateTime.UtcNow,
                SecurityToken = Guid.NewGuid().ToString(),
                TwoFactorRequired = true,
            };

            var houseHold = new HouseHold()
            {
                Id = Guid.NewGuid(),
                CreatedBy = user.Id,
                CreatedOn = DateTime.UtcNow,
                ManOfTheHouse = user.Id,
                Name = $"{user.FirstName}'s House"
            };
            user.HouseHoldId = houseHold.Id;


            _databaseContext.HouseHolds.Add(houseHold);
            _databaseContext.Users.Add(user);
            await _databaseContext.SaveChangesAsync();

            _logger.LogInformation(LogEvents.CreatingUser, "Successfully created a user {userId} with email address {emailAddress}",
                user.Id, model.EmailAddress);

            return new AuthResult
            {
                Result =SignInResult.Success,
                Token = user.Id.ToString()
            };
        }

        /// <summary>
        /// Creates the users identity and returns it
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        public async Task<ClaimsIdentity> CreateUserClaimsIdentityAsync(Guid clientId)
        {
            if (clientId == null || Guid.Empty == clientId)
                throw new ArgumentNullException($"{nameof(clientId)}");

            var id = new ClaimsIdentity("JWT", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            var secKey = await GetSecurityStampAsync(clientId);

            var user = await FindUserById(clientId);

            // Add default claims
            id.AddClaim(new Claim(ClaimTypes.NameIdentifier, clientId.ToString(), ClaimValueTypes.String));
            id.AddClaim(new Claim(ClaimTypes.Name, user.Email, ClaimValueTypes.String));
            id.AddClaim(new Claim(SecurityStampClaimType, secKey, ClaimValueTypes.String));

            return id;
        }

        /// <summary>
        /// Retrieves all the roles the given user has
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        private async Task<Dictionary<string, object>> GetRolesAsync(Guid clientId)
        {
            User user = await GetUser(clientId);

            return new Dictionary<string, object>
            {
                [ClaimsIdentity.DefaultRoleClaimType] = "User"
            };
        }

        /// <summary>
        /// Retrieves the users security toekn
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        private async Task<string> GetSecurityStampAsync(Guid clientId)
        {
            User user = await GetUser(clientId);

            return user.SecurityToken.ToString();
        }

        /// <summary>
        /// Retrievs the user with the given Id
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        private async Task<User> GetUser(Guid clientId)
        {
            var user = await _databaseContext.Users.FindAsync(clientId);

            if (user == null)
                throw new InvalidOperationException($"Could not find the user with id {clientId}");
            return user;
        }

        /// <summary>
        /// Generates 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private async Task<string> GenerateTokenAsync(User user)
        {
            // Create identity for the client
            var claims = await CreateUserClaimsIdentityAsync(user.Id);
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtTokenConfig.Secret));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                IssuedAt = DateTime.UtcNow,
                Subject = new ClaimsIdentity(claims),
                Claims = await GetRolesAsync(user.Id),
                Audience = _jwtTokenConfig.Audience,
                Issuer = _jwtTokenConfig.Issuer,
                Expires = DateTime.UtcNow.AddMinutes(_jwtTokenConfig.AccessExpiration),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512),
            };

            var token = new JwtSecurityTokenHandler().CreateJwtSecurityToken(tokenDescriptor);

            // Create a header and add it to the request pipeline 
            var bearer = new JwtSecurityTokenHandler().WriteToken(token);

            return bearer;
        }

        /// <summary>
        /// Checks if the specified user needs two factor
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private bool NeedsTwoFactor(User user)
            => user.TwoFactorRequired;


        /// <summary>
        /// Checks if the users password matches the one provided
        /// </summary>
        /// <param name="user"> The users instance </param>
        /// <param name="password"> The provided user password</param>
        /// <returns></returns>
        private bool ValidatePassword(User user, string password)
            => _passwordHasher.VerifyHashedPassword(user.Hash, password);

        /// <summary>
        /// Performs pre login checks on the account and returns the result
        /// </summary>
        /// <param name="user"> The user instance</param>
        /// <returns></returns>
        private protected SignInResult PreLoginChecks(User user)
        {
            if (!CanSignIn(user))
                return SignInResult.NotAllowed;
            if (IsLockedOut(user))
                return SignInResult.LockedOut;

            return SignInResult.Success;
        }

        /// <summary>
        /// Returns true if the users account is valid
        /// </summary>
        /// <param name="user"> User instance</param>
        /// <returns></returns>
        private bool CanSignIn(User user)
            => !user.AccountClosed;

        /// <summary>
        /// Returns true if the user is locked out and false otherwsie
        /// </summary>
        /// <param name="user"> User instance</param>
        /// <returns></returns>
        private bool IsLockedOut(User user)
        {
            if (user.LockoutEndDate.HasValue && user.LockoutEndDate.Value < DateTime.UtcNow)
                return user.IsLockedOut;

            return false;
        }

        public void Dispose()
        {
            _databaseContext.Dispose();
        }
    }
}
