using Microsoft.AspNetCore.Identity;

namespace DataAccessLayer.Entities
{
    /// <summary>
    ///   It describes a user entity
    /// </summary>
    public class User : IdentityUser<int>
    {
        /// <summary>Gets or sets the first name.</summary>
        /// <value>The first name.</value>
        public string FirstName { get; set; }

        /// <summary>Gets or sets the last name.</summary>
        /// <value>The last name.</value>
        public string LastName { get; set; }

        /// <summary>Gets or sets the refresh token.</summary>
        /// <value>The refresh token.</value>
        public string? RefreshToken { get; set; }

        /// <summary>Gets or sets the refresh token expiry time.</summary>
        /// <value>The refresh token expiry time.</value>
        public DateTime? RefreshTokenExpiryTime { get; set; }

        /// <summary>Gets or sets the user projects.</summary>
        /// <value>The user projects.</value>
        public virtual ICollection<UserProject> UserProjects { get; set; }

        /// <summary>Gets or sets the tasks.</summary>
        /// <value>The tasks.</value>
        public virtual ICollection<Assignment> Tasks { get; set; }
    }
}
