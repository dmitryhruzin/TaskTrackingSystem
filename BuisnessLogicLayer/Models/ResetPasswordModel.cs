namespace BuisnessLogicLayer.Models
{
    /// <summary>
    ///   Describes a resetPasswordModel
    /// </summary>
    public class ResetPasswordModel
    {
        /// <summary>Gets or sets the password.</summary>
        /// <value>The password.</value>
        public string Password { get; set; }

        /// <summary>Gets or sets the confirm password.</summary>
        /// <value>The confirm password.</value>
        public string ConfirmPassword { get; set; }

        /// <summary>Gets or sets the email.</summary>
        /// <value>The email.</value>
        public string Email { get; set; }

        /// <summary>Gets or sets the token.</summary>
        /// <value>The token.</value>
        public string Token { get; set; }
    }
}
