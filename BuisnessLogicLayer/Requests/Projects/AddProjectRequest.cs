using MediatR;

namespace BuisnessLogicLayer.Requests.Projects;

public record AddProjectRequest(
    string Name,
    string Description,
    DateTime StartDate,
    DateTime ExpiryDate,
    int StatusId) : IRequest;