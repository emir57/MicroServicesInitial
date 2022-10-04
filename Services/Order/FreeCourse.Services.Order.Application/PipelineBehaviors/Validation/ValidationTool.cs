using FluentValidation;
using FluentValidation.Results;

namespace FreeCourse.Services.Order.Application.PipelineBehaviors.Validation;

public class ValidationTool
{
    public static void Validate(IValidator validator, object entity)
    {
        ValidationContext<object> context = new(entity);
        ValidationResult result = validator.Validate(context);
        if (result.IsValid == false)
            throw new ValidationException(result.Errors);
    }
}
