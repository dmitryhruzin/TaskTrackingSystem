﻿namespace BuisnessLogicLayer.Models
{
    /// <summary>
    ///   Describes a tokenOptions
    /// </summary>
    public class TokenOptions
    {
        /// <summary>Gets or sets the secret.</summary>
        /// <value>The secret.</value>
        public string Secret { get; set; }

        /// <summary>Gets or sets the issuer.</summary>
        /// <value>The issuer.</value>
        public string Issuer { get; set; }

        /// <summary>Gets or sets the audience.</summary>
        /// <value>The audience.</value>
        public string Audience { get; set; }
    }
}
