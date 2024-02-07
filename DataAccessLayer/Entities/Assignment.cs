namespace DataAccessLayer.Entities
{
    /// <summary>
    ///   It describes an assignment entity
    /// </summary>
    public class Assignment : BaseEntity
    {
        /// <summary>Gets or sets the name.</summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>Gets or sets the description.</summary>
        /// <value>The description.</value>
        public string Description { get; set; }

        /// <summary>Gets or sets the start date.</summary>
        /// <value>The start date.</value>
        public DateTime StartDate { get; set; }

        /// <summary>Gets or sets the expiry date.</summary>
        /// <value>The expiry date.</value>
        public DateTime ExpiryDate { get; set; }

        /// <summary>Gets or sets the manager identifier.</summary>
        /// <value>The manager identifier.</value>
        public int? ManagerId { get; set; }

        /// <summary>Gets or sets the status identifier.</summary>
        /// <value>The status identifier.</value>
        public int StatusId { get; set; }

        /// <summary>Gets or sets the project identifier.</summary>
        /// <value>The project identifier.</value>
        public int ProjectId { get; set; }

        /// <summary>Gets or sets the manager.</summary>
        /// <value>The manager.</value>
        public User Manager { get; set; }

        /// <summary>Gets or sets the status.</summary>
        /// <value>The status.</value>
        public AssignmentStatus Status { get; set; }

        /// <summary>Gets or sets the user projects.</summary>
        /// <value>The user projects.</value>
        public ICollection<UserProject> UserProjects { get; set; }

        /// <summary>Gets or sets the project.</summary>
        /// <value>The project.</value>
        public Project Project { get; set; }
    }
}
