namespace DataAccessLayer.Entities
{
    /// <summary>
    ///   It describes a project entity
    /// </summary>
    public class Project : BaseEntity
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

        /// <summary>Gets or sets the status identifier.</summary>
        /// <value>The status identifier.</value>
        public int StatusId { get; set; }

        /// <summary>Gets or sets the status.</summary>
        /// <value>The status.</value>
        public ProjectStatus Status { get; set; }

        /// <summary>Gets or sets the tasks.</summary>
        /// <value>The tasks.</value>
        public ICollection<Assignment> Tasks { get; set; }
    }
}
