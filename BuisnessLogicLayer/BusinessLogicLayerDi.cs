using BuisnessLogicLayer.Commands.Assignments.AddAssignment;
using BuisnessLogicLayer.Interfaces;
using BuisnessLogicLayer.Models;
using BuisnessLogicLayer.Services;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BuisnessLogicLayer;

public static class BusinessLogicLayerDi
{
    public static void RegisterBusinessLogicLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddFluentValidators();
        services.AddMediator();
        services.AddMemoryCache();
        services.AddAutoMapper(typeof(BusinessLogicLayerDi).Assembly);
        services.AddAppSettings(configuration);
        services.AddBusinessServices();
    }

    private static void AddMediator(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(BusinessLogicLayerDi).Assembly));
    }

    private static void AddFluentValidators(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<AddAssignmentRequestValidator>();
    }

    private static void AddAppSettings(this IServiceCollection services, IConfiguration configuration)
    {
        var emailOptionsConfiguration = configuration.GetSection("Email");

        services.Configure<EmailOptions>(emailOptionsConfiguration);

        var tokenOptionsConfiguration = configuration.GetSection("Token");

        services.Configure<TokenOptions>(tokenOptionsConfiguration);
    }

    private static void AddBusinessServices(this IServiceCollection services)
    {
        services.AddScoped<IAssignmentService, AssignmentService>();
        services.AddScoped<IAssignmentStatusService, AssignmentStatusService>(); 
        services.AddScoped<IEmailService, EmailService>(); 
        services.AddScoped<IGenerateTokenService, GenerateTokenService>(); 
        services.AddScoped<IHashingService, HashingService>();
        services.AddScoped<IPositionService, PositionService>();
        services.AddScoped<IProjectService, ProjectService>(); 
        services.AddScoped<IProjectStatusService, ProjectStatusService>(); 
        services.AddScoped<IRoleService, RoleService>(); 
        services.AddScoped<IUserProjectService, UserProjectService>(); 
        services.AddScoped<IUserService, UserService>(); 
    }

}