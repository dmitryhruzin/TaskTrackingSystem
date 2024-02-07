using AutoMapper;
using BuisnessLogicLayer.Interfaces;
using BuisnessLogicLayer.Requests.Assignments;
using FluentValidation;
using MediatR;

namespace BuisnessLogicLayer.Commands.Assignments.AddAssignment;

public class AddAssignmentCommandHandler : IRequestHandler<AddAssignmentRequest>
{
    private readonly IMapper _mapper;
    private readonly IValidator<AddAssignmentRequest> _validator;
    private readonly IAssignmentService _assignmentService;

    public AddAssignmentCommandHandler(
        IMapper mapper,
        IValidator<AddAssignmentRequest> validator,
        IAssignmentService assignmentService)
    {
        _mapper = mapper;
        _validator = validator;
        _assignmentService = assignmentService;
    }
    
    public async Task Handle(AddAssignmentRequest request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var model = _mapper.Map<Assignment>(request);

        await _assignmentService.AddAsync(model, cancellationToken);
    }
}