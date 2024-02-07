using BuisnessLogicLayer.Responses.ProjectStatuses;
using MediatR;

namespace BuisnessLogicLayer.Requests.ProjectStatuses;

public record GetProjectStatusesRequest() : IRequest<GetProjectStatusesResponse>;