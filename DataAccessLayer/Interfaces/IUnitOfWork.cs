namespace DataAccessLayer.Interfaces;

public interface IUnitOfWork
{
    IAssignmentRepository AssignmentRepository { get; }
    
    IAssignmentStatusRepository AssignmentStatusRepository { get; }
    
    IPositionRepository PositionRepository { get; }
    
    IProjectRepository ProjectRepository { get; }
    
    IProjectStatusRepository ProjectStatusRepository { get; }
    
    IUserProjectRepository UserProjectRepository { get; }
    
    IUserRepository UserRepository { get; }
    
    IRoleRepository RoleRepository { get; }
    
    Task SaveAsync(CancellationToken cancellationToken);
}