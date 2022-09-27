using MediatR;
using System.Diagnostics;

namespace FreeCourse.Services.Order.Application.PipelineBehaviors.Logging;

public class LogPipeline<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>, ILoggableRequest
{
    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        Debug.WriteLine("Request is logged");
        return await next();
    }
}
