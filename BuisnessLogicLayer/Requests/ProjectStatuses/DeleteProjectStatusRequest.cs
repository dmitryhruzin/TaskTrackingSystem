using MediatR;

namespace BuisnessLogicLayer.Requests.ProjectStatuses;

public record DeleteProjectStatusRequest(int Id) : IRequest;