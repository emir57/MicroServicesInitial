using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace FreeCourse.Services.Order.Application.PipelineBehaviors.Validation;

public class ValidationPipelineBehavior<TRequest, TResponse> : BasePipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationPipelineBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    protected override void OnBefore(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        ValidationContext<object> context = new(request);
        List<ValidationFailure> failures = _validators
                                            .Select(validator => validator.Validate(context))
                                            .SelectMany(result => result.Errors)
                                            .Where(failure => failure != null)
                                            .ToList();
        if (failures.Count != 0) throw new ValidationException(failures);
    }
}
