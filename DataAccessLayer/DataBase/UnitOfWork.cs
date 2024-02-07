using DataAccessLayer.Interfaces;

namespace DataAccessLayer.DataBase;

public class UnitOfWork : IUnitOfWork
{
    private readonly TaskDbContext _dbContext;
    private readonly IAssignmentRepository _assignmentRepository;
    private readonly IAssignmentStatusRepository _assignmentStatusRepository;
    private readonly IPositionRepository _positionRepository;
    private readonly IProjectRepository _projectRepository;
    private readonly IProjectStatusRepository _projectStatusRepository;
    private readonly IUserProjectRepository _userProjectRepository;
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;

    public UnitOfWork(
        TaskDbContext dbContext,
        IAssignmentRepository assignmentRepository,
        IAssignmentStatusRepository assignmentStatusRepository,
        IPositionRepository positionRepository,
        IProjectRepository projectRepository,
        IProjectStatusRepository projectStatusRepository,
        IUserProjectRepository userProjectRepository,
        IUserRepository userRepository,
        IRoleRepository roleRepository)
    {
        _dbContext = dbContext;
        _assignmentRepository = assignmentRepository;
        _assignmentStatusRepository = assignmentStatusRepository;
        _positionRepository = positionRepository;
        _projectRepository = projectRepository;
        _projectStatusRepository = projectStatusRepository;
        _userProjectRepository = userProjectRepository;
        _userRepository = userRepository;
        _roleRepository = roleRepository;
    }

    public IAssignmentRepository AssignmentRepository => _assignmentRepository;
    
    public IAssignmentStatusRepository AssignmentStatusRepository => _assignmentStatusRepository;

    public IPositionRepository PositionRepository => _positionRepository;

    public IProjectRepository ProjectRepository => _projectRepository;

    public IProjectStatusRepository ProjectStatusRepository => _projectStatusRepository;

    public IUserProjectRepository UserProjectRepository => _userProjectRepository;

    public IUserRepository UserRepository => _userRepository;

    public IRoleRepository RoleRepository => _roleRepository;

    public async Task SaveAsync(CancellationToken cancellationToken)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}