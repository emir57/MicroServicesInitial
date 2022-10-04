using FreeCourse.Shared.CrossCuttingConcerns;
using FreeCourse.Shared.Messages;
using MassTransit;
using MediatR;
using System.Reflection;

namespace FreeCourse.Services.Order.Application.PipelineBehaviors.Logging;

public class LoggingBehavior<TRequest, TResponse> : BasePipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>, ILoggableRequest
{
    private readonly IPublishEndpoint _publishEndpoint;
    public LoggingBehavior(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    protected async override void OnBefore(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        LogEvent logEvent = getMethodDetail(request);
        await _publishEndpoint.Publish<LogEvent>(logEvent);
    }


    private LogEvent getMethodDetail(TRequest request)
    {
        return new LogEvent
        {
            Parameters = getParameters(request),
            MethodName = request.GetType().FullName
        };
    }
    private List<LogParameter> getParameters(object obj)
    {
        PropertyInfo[]? properties = obj.GetType().GetProperties();
        return obj.GetType().GetProperties().Select(x => new LogParameter
        {
            Name = x.Name,
            Type = x.GetType().ToString(),
            Value = x.GetValue(obj)
        }).ToList();
    }
}
