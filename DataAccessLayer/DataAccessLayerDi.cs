using DataAccessLayer.DataBase;
using DataAccessLayer.DataBase.Repositories;
using DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccessLayer;

public static class DataAccessLayerDi
{
    public static void RegisterDataAccessLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.SetupDataBase(configuration);
        services.AddRepositories();
        services.AddUnitOfWork();
    }

    private static void SetupDataBase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<TaskDbContext>(opts =>
            opts.UseSqlite(configuration.GetConnectionString("DefaultConnection")!)); 
    }

    private static void AddUnitOfWork(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>(); 
    }

    private static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IAssignmentRepository, AssignmentRepository>();
        services.AddScoped<IAssignmentStatusRepository, AssignmentStatusRepository>();
        services.AddScoped<IPositionRepository, PositionRepository>();
        services.AddScoped<IProjectRepository, ProjectRepository>();
        services.AddScoped<IProjectStatusRepository, ProjectStatusRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<IUserProjectRepository, UserProjectRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
    }
}