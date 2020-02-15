using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Whose_Turn.Models;
using Whose_Turn.Models.Account;
using Microsoft.AspNetCore.Identity;
using Whose_Turn.Managers;
using NServiceBus;
using System.Net;
using Whose_Turn.Servicebus;
using AutoMapper;
using Whose_Turn.Context.Entities;
using Microsoft.AspNetCore.Authorization;

namespace Whose_Turn.Controllers
{
    [Route("api/[controller]/[action]")]
    [Authorize]
    [ApiController]
    public class AccountController : BaseController
    {
        private static class LogEvents
        {
            public static readonly EventId CreatingUser = new EventId(1, nameof(NewUser));
        }

        private readonly ILogger _logger;
        private readonly WhoseTurnUserManager _userManager;

        private readonly IEndpointInstance _endpointInstance;
        private readonly IMapper _userProfileMapper;

        public AccountController(IServiceProvider provider)
        {
            _userManager = provider.GetService<WhoseTurnUserManager>();
            _logger = provider.GetService<ILogger<AccountController>>();

            _userProfileMapper = new MapperConfiguration(cfg => cfg.CreateMap<User, ProfileModel>()).CreateMapper();
            _endpointInstance = provider.GetService<IEndpointInstance>();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> NewUser(CreateUserModel model)
        {
            var result = await _userManager.CreateUser(model);

            if (result.Result != Microsoft.AspNetCore.Identity.SignInResult.Success)
            {
                return new JsonHttpStatusResult(new ErrorModel()
                {
                    Status = (int)HttpStatusCode.Conflict,
                    Title = result.Token,
                    TraceId = HttpContext.TraceIdentifier,

                }, HttpStatusCode.Conflict);
            }

            var userId = Guid.Parse(result.Token);

            await _endpointInstance.Send(new SendVerifyEmail()
            {
                UserId = userId
            });

            _logger.LogInformation(LogEvents.CreatingUser, "Sucessfully place verify email message to on service bus for email {emailAddress}",
                model.EmailAddress);

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<User, ProfileModel>())
                .CreateMapper();

            var user = await _userManager.FindUserById(userId);
            return Json(mapper.Map<ProfileModel>(user));
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Token(LoginModel model)
        {
            var result = await _userManager.SignIn(model);

            if (result.Result == Microsoft.AspNetCore.Identity.SignInResult.Success)
            {
                return Json(result);
            }

            return new JsonHttpStatusResult(result, HttpStatusCode.Unauthorized);
        }

        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var user = await _userManager.FindUserById(UserId);

            return Json(_userProfileMapper.Map<ProfileModel>(user));
        }
    }
}
