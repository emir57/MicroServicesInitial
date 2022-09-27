using MediatR;
using System.Diagnostics;

namespace FreeCourse.Services.Order.Application.PipelineBehaviors.Performance;

public class PerformancingRequest<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>, IPerformanceRequest
{
    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        Stopwatch sw = Stopwatch.StartNew();
        TResponse response = await next();
        sw.Stop();

        if (sw.ElapsedMilliseconds > request.Interval)
            Debug.WriteLine($"{sw.Elapsed.TotalMilliseconds} MS");

        return response;
    }
}
