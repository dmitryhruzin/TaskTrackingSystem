namespace BuisnessLogicLayer.Interfaces;

public interface IUserProjectService : ICrud<UserProject>
{
    Task AddUserProjectsAsync(IEnumerable<UserProject> models, CancellationToken cancellationToken);
    
    Task DeleteUserProjectsAsync(IEnumerable<int> ids, CancellationToken cancellationToken);
}