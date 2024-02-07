namespace BuisnessLogicLayer.Models
{
    /// <summary>
    ///   Describes a taskModel
    /// </summary>
    public class TaskModel
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

        /// <summary>Gets or sets the name of the manager user.</summary>
        /// <value>The name of the manager user.</value>
        public string? ManagerUserName { get; set; }

        /// <summary>Gets or sets the manager identifier.</summary>
        /// <value>The manager identifier.</value>
        public int? ManagerId { get; set; }

        /// <summary>Gets or sets the name of the status.</summary>
        /// <value>The name of the status.</value>
        public string? StatusName { get; set; }

        /// <summary>Gets or sets the status identifier.</summary>
        /// <value>The status identifier.</value>
        public int StatusId { get; set; }

        /// <summary>Gets or sets the name of the project.</summary>
        /// <value>The name of the project.</value>
        public string? ProjectName { get; set; }

        /// <summary>Gets or sets the project identifier.</summary>
        /// <value>The project identifier.</value>
        public int ProjectId { get; set; }

        /// <summary>Gets or sets the user project ids.</summary>
        /// <value>The user project ids.</value>
        public ICollection<int>? UserProjectIds { get; set; }
    }
}
