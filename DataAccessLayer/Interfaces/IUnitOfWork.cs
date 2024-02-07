namespace DataAccessLayer.Interfaces
{
    /// <summary>
    ///   Describes an unit of work
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>Gets the assignment repository.</summary>
        /// <value>The assignment repository.</value>
        IAssignmentRepository AssignmentRepository { get; }

        /// <summary>Gets the assignment status repository.</summary>
        /// <value>The assignment status repository.</value>
        IAssignmentStatusRepository AssignmentStatusRepository { get; }

        /// <summary>Gets the position repository.</summary>
        /// <value>The position repository.</value>
        IPositionRepository PositionRepository { get; }

        /// <summary>Gets the project repository.</summary>
        /// <value>The project repository.</value>
        IProjectRepository ProjectRepository { get; }

        /// <summary>Gets the project status repository.</summary>
        /// <value>The project status repository.</value>
        IProjectStatusRepository ProjectStatusRepository { get; }

        /// <summary>Gets the user project repository.</summary>
        /// <value>The user project repository.</value>
        IUserProjectRepository UserProjectRepository { get; }

        /// <summary>Saves asynchronous.</summary>
        Task SaveAsync();
    }
}
