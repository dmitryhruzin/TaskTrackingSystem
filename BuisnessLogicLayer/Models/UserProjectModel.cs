namespace BuisnessLogicLayer.Models
{
    /// <summary>
    ///   Describes a userProjectModel
    /// </summary>
    public class UserProjectModel
    {
        /// <summary>Gets or sets the identifier.</summary>
        /// <value>The identifier.</value>
        public int Id { get; set; }

        /// <summary>Gets or sets the name of the user.</summary>
        /// <value>The name of the user.</value>
        public string? UserName { get; set; }

        /// <summary>Gets or sets the user email.</summary>
        /// <value>The user email.</value>
        public string UserEmail { get; set; }

        /// <summary>Gets or sets the user identifier.</summary>
        /// <value>The user identifier.</value>
        public int UserId { get; set; }

        /// <summary>Gets or sets the name of the task.</summary>
        /// <value>The name of the task.</value>
        public string? TaskName { get; set; }

        /// <summary>Gets or sets the task identifier.</summary>
        /// <value>The task identifier.</value>
        public int TaskId { get; set; }

        /// <summary>Gets or sets the name of the position.</summary>
        /// <value>The name of the position.</value>
        public string? PositionName { get; set; }

        /// <summary>Gets or sets the position identifier.</summary>
        /// <value>The position identifier.</value>
        public int PositionId { get; set; }
    }
}
