namespace DataAccessLayer.Entities
{
    /// <summary>
    ///   It describes a position entity
    /// </summary>
    public class Position : BaseEntity
    {
        /// <summary>Gets or sets the name.</summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>Gets or sets the description.</summary>
        /// <value>The description.</value>
        public string Description { get; set; }

        /// <summary>Gets or sets the user projects.</summary>
        /// <value>The user projects.</value>
        public ICollection<UserProject> UserProjects { get; set; }
    }
}
