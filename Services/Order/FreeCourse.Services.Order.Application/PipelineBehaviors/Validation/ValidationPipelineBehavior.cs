using MediatR;

namespace FreeCourse.Services.Order.Application.PipelineBehaviors.Validation;

public class ValidationPipelineBehavior<TRequest, TResponse> : BasePipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    //private readonly

    protected override void OnBefore(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        
    }
}
