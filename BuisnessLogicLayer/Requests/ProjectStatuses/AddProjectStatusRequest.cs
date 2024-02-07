using MediatR;

namespace BuisnessLogicLayer.Requests.ProjectStatuses;

public record AddProjectStatusRequest(string Name) : IRequest;