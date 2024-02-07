using AutoMapper;
using BuisnessLogicLayer.Interfaces;
using BuisnessLogicLayer.Requests.AssignmentStatuses;
using FluentValidation;
using MediatR;

namespace BuisnessLogicLayer.Commands.AssignmentStatuses.AddAssignmentStatus;

public class AddAssignmentStatusCommandHandler : IRequestHandler<AddAssignmentStatusRequest>
{
    private readonly IMapper _mapper;
    private readonly IAssignmentStatusService _assignmentStatusService;
    private readonly IValidator<AddAssignmentStatusRequest> _validator;

    public AddAssignmentStatusCommandHandler(
        IMapper mapper,
        IAssignmentStatusService assignmentStatusService,
        IValidator<AddAssignmentStatusRequest> validator)
    {
        _mapper = mapper;
        _assignmentStatusService = assignmentStatusService;
        _validator = validator;
    }
    
    public async Task Handle(AddAssignmentStatusRequest request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var model = _mapper.Map<AssignmentStatus>(request);

        await _assignmentStatusService.AddAsync(model, cancellationToken);
    }
}