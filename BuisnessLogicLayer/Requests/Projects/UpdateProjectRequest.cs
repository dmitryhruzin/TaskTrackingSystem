using MediatR;

namespace BuisnessLogicLayer.Requests.Projects;

public record UpdateProjectRequest(
    int Id,
    string Name,
    string Description,
    DateTime StartDate,
    DateTime ExpiryDate,
    int StatusId) : IRequest;