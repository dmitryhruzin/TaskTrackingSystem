namespace BuisnessLogicLayer.Models
{
    /// <summary>
    ///   Describes a registerUserModel
    /// </summary>
    public class RegisterUserModel
    {
        /// <summary>Gets or sets the first name.</summary>
        /// <value>The first name.</value>
        public string FirstName { get; set; }

        /// <summary>Gets or sets the last name.</summary>
        /// <value>The last name.</value>
        public string LastName { get; set; }

        /// <summary>Gets or sets the name of the user.</summary>
        /// <value>The name of the user.</value>
        public string UserName { get; set; }

        /// <summary>Gets or sets the password.</summary>
        /// <value>The password.</value>
        public string Password { get; set; }

        /// <summary>Gets or sets the confirm password.</summary>
        /// <value>The confirm password.</value>
        public string ConfirmPassword { get; set; }

        /// <summary>Gets or sets the email.</summary>
        /// <value>The email.</value>
        public string Email { get; set; }

        /// <summary>Gets or sets the role.</summary>
        /// <value>The role.</value>
        public string? Role { get; set; }
    }
}