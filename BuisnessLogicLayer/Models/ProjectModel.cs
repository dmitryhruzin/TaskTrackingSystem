namespace BuisnessLogicLayer.Models
{
    /// <summary>
    ///   Describes a projectModel
    /// </summary>
    public class ProjectModel
    {
        /// <summary>Gets or sets the identifier.</summary>
        /// <value>The identifier.</value>
        public int Id { get; set; }

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

        /// <summary>Gets or sets the name of the status.</summary>
        /// <value>The name of the status.</value>
        public string? StatusName { get; set; }

        /// <summary>Gets or sets the status identifier.</summary>
        /// <value>The status identifier.</value>
        public int StatusId { get; set; }

        /// <summary>Gets or sets the task ids.</summary>
        /// <value>The task ids.</value>
        public ICollection<int>? TaskIds { get; set; }
    }
}
