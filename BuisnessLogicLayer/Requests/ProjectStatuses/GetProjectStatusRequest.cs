using BuisnessLogicLayer.Responses.ProjectStatuses;
using MediatR;

namespace BuisnessLogicLayer.Requests.ProjectStatuses;

public record GetProjectStatusRequest(int Id) : IRequest<GetProjectStatusResponse>;