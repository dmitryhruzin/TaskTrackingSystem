namespace DataAccessLayer.Entities
{
    /// <summary>
    ///   It describes a project status entity
    /// </summary>
    public class ProjectStatus : BaseEntity
    {
        /// <summary>Gets or sets the name.</summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>Gets or sets the projects.</summary>
        /// <value>The projects.</value>
        public ICollection<Project> Projects { get; set; }
    }
}
