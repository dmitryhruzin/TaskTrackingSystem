namespace DataAccessLayer.Entities
{
    /// <summary>
    ///   It describes an assignment status entity
    /// </summary>
    public class AssignmentStatus : BaseEntity
    {
        /// <summary>Gets or sets the name.</summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>Gets or sets the tasks.</summary>
        /// <value>The tasks.</value>
        public ICollection<Assignment> Tasks { get; set; }
    }
}
