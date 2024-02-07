using DataAccessLayer.DataBase.Repositories;
using DataAccessLayer.Interfaces;

namespace DataAccessLayer.DataBase
{
    /// <summary>
    ///   Implements an unit of work
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        readonly TaskDbContext dbContext;

        AssignmentRepository assignmentRepository;

        AssignmentStatusRepository assignmentStatusRepository;

        PositionRepository positionRepository;

        ProjectRepository projectRepository;

        ProjectStatusRepository projectStatusRepository;

        UserProjectRepository userProjectRepository;

        /// <summary>Initializes a new instance of the <see cref="UnitOfWork" /> class.</summary>
        /// <param name="dbContext">The database context.</param>
        public UnitOfWork(TaskDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <summary>Gets the assignment repository.</summary>
        /// <value>The assignment repository.</value>
        public IAssignmentRepository AssignmentRepository => assignmentRepository ??= new(dbContext);

        /// <summary>Gets the assignment status repository.</summary>
        /// <value>The assignment status repository.</value>
        public IAssignmentStatusRepository AssignmentStatusRepository => assignmentStatusRepository ??= new(dbContext);

        /// <summary>Gets the position repository.</summary>
        /// <value>The position repository.</value>
        public IPositionRepository PositionRepository => positionRepository ??= new(dbContext);

        /// <summary>Gets the project repository.</summary>
        /// <value>The project repository.</value>
        public IProjectRepository ProjectRepository => projectRepository ??= new(dbContext);

        /// <summary>Gets the project status repository.</summary>
        /// <value>The project status repository.</value>
        public IProjectStatusRepository ProjectStatusRepository => projectStatusRepository ??= new(dbContext);

        /// <summary>Gets the user project repository.</summary>
        /// <value>The user project repository.</value>
        public IUserProjectRepository UserProjectRepository => userProjectRepository ??= new(dbContext);

        /// <summary>Saves asynchronous.</summary>
        public async Task SaveAsync()
        {
            await dbContext.SaveChangesAsync();
        }
    }
}
