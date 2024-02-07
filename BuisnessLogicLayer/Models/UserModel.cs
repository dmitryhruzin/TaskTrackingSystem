namespace BuisnessLogicLayer.Models
{
    /// <summary>
    ///   Describes a userModel
    /// </summary>
    public class UserModel
    {
        /// <summary>Gets or sets the identifier.</summary>
        /// <value>The identifier.</value>
        public int Id { get; set; }

        /// <summary>Gets or sets the first name.</summary>
        /// <value>The first name.</value>
        public string FirstName { get; set; }

        /// <summary>Gets or sets the last name.</summary>
        /// <value>The last name.</value>
        public string LastName { get; set; }

        /// <summary>Gets or sets the name of the user.</summary>
        /// <value>The name of the user.</value>
        public string UserName { get; set; }

        /// <summary>Gets or sets the email.</summary>
        /// <value>The email.</value>
        public string Email { get; set; }

        /// <summary>Gets or sets the user project ids.</summary>
        /// <value>The user project ids.</value>
        public ICollection<int> UserProjectIds { get; set; }

        /// <summary>Gets or sets the task ids.</summary>
        /// <value>The task ids.</value>
        public ICollection<int> TaskIds { get; set; }
    }
}
