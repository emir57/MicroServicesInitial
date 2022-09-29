using MassTransit;
using MediatR;
using System.Diagnostics;

namespace FreeCourse.Services.Order.Application.PipelineBehaviors.Performance;

public class PerformancePipelineBehavior<TRequest, TResponse> : BasePipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>, IPerformanceRequest
{
    private Stopwatch _stopwatch;
    public PerformancePipelineBehavior()
    {

    }

    protected override void OnBefore(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        _stopwatch = Stopwatch.StartNew();
    }

    protected override void OnAfter(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        _stopwatch.Stop();

        if (_stopwatch.ElapsedMilliseconds > request.Interval)
        {
            Debug.WriteLine($"Performance: {_stopwatch.Elapsed.TotalMilliseconds} MS");
        }
    }
}
