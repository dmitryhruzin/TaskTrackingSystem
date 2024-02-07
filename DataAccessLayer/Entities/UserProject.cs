namespace DataAccessLayer.Entities
{
    /// <summary>
    ///   It describes a userproject entity
    /// </summary>
    public class UserProject : BaseEntity
    {
        /// <summary>Gets or sets the user identifier.</summary>
        /// <value>The user identifier.</value>
        public int UserId { get; set; }

        /// <summary>Gets or sets the task identifier.</summary>
        /// <value>The task identifier.</value>
        public int TaskId { get; set; }

        /// <summary>Gets or sets the position identifier.</summary>
        /// <value>The position identifier.</value>
        public int PositionId { get; set; }

        /// <summary>Gets or sets the user.</summary>
        /// <value>The user.</value>
        public User User { get; set; }

        /// <summary>Gets or sets the task.</summary>
        /// <value>The task.</value>
        public Assignment Task { get; set; }

        /// <summary>Gets or sets the position.</summary>
        /// <value>The position.</value>
        public Position Position { get; set; }
    }
}
