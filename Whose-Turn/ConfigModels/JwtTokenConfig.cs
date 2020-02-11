using System;
namespace Whose_Turn.ConfigModels
{
    public class JwtTokenConfig
    {
        /// <summary>
        /// Gets or sets the secret
        /// </summary>
        public string Secret { get; set; }

        /// <summary>
        /// Gets or sets the issuer
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// Gets or sets the token audiences
        /// </summary>
        public string Audience { get; set; }

        /// <summary>
        /// Gets or sets the access token
        /// </summary>
        public int AccessExpiration { get; set; }

        /// <summary>
        /// Gets or sets the refresh expiration
        /// </summary>
        public int RefreshExpiration { get; set; }
    }
}
