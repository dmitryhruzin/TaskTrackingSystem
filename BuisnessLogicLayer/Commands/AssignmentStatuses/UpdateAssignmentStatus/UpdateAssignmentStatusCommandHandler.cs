using AutoMapper;
using BuisnessLogicLayer.Interfaces;
using BuisnessLogicLayer.Requests.AssignmentStatuses;
using FluentValidation;
using MediatR;

namespace BuisnessLogicLayer.Commands.AssignmentStatuses.UpdateAssignmentStatus;

public class UpdateAssignmentStatusCommandHandler : IRequestHandler<UpdateAssignmentStatusRequest>
{
    private readonly IMapper _mapper;
    private readonly IAssignmentStatusService _assignmentStatusService;
    private readonly IValidator<UpdateAssignmentStatusRequest> _validator;

    public UpdateAssignmentStatusCommandHandler(
        IMapper mapper,
        IAssignmentStatusService assignmentStatusService,
        IValidator<UpdateAssignmentStatusRequest> validator)
    {
        _mapper = mapper;
        _assignmentStatusService = assignmentStatusService;
        _validator = validator;
    }
    
    public async Task Handle(UpdateAssignmentStatusRequest request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var model = _mapper.Map<AssignmentStatus>(request);

        await _assignmentStatusService.UpdateAsync(model, cancellationToken);
    }
}