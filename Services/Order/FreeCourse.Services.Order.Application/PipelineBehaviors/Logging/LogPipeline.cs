using FreeCourse.Shared.CrossCuttingConcerns;
using FreeCourse.Shared.Messages;
using MassTransit;
using MediatR;

namespace FreeCourse.Services.Order.Application.PipelineBehaviors.Logging;

public class LogPipeline<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>, ILoggableRequest
{
    private readonly IPublishEndpoint _publishEndpoint;
    public LogPipeline(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }
    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        LogDetail logDetail = new()
        {
            MethodName = next.Method.Name,
            Parameters = getParameters(next)
        };
        await _publishEndpoint.Publish<LogEvent>(logDetail);
        return await next();
    }

    private List<LogParameter> getParameters(RequestHandlerDelegate<TResponse> next)
    {
        return next.Method.GetParameters().Select(x => new LogParameter
        {
            Name = x.Name,
            Type = x.GetType().ToString(),
            Value = x.DefaultValue
        }).ToList();
    }
}
