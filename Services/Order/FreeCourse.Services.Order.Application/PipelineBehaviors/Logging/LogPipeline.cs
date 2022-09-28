using FreeCourse.Shared.CrossCuttingConcerns;
using FreeCourse.Shared.CrossCuttingConcerns.Serilog;
using MediatR;
using System.Diagnostics;
using System.Reflection;
using System.Text.Json;

namespace FreeCourse.Services.Order.Application.PipelineBehaviors.Logging;

public class LogPipeline<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>, ILoggableRequest
{
    LoggerServiceBase _loggerServiceBase;
    public LogPipeline(LoggerServiceBase loggerServiceBase)
    {
        _loggerServiceBase = loggerServiceBase;
    }
    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        LogDetail logDetail = new()
        {
            MethodName = next.Method.Name,
            Parameters = getParameters(next)
        };
        _loggerServiceBase.Info(JsonSerializer.Serialize(logDetail));
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
