using MediatR;

namespace BuisnessLogicLayer.Requests.ProjectStatuses;

public record UpdateProjectStatusRequest(int Id, string Name) : IRequest;