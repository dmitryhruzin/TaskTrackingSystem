using MediatR;

namespace BuisnessLogicLayer.Requests.Assignments;

public record DeleteAssignmentRequest(int Id) : IRequest;