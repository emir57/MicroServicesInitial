using FreeCourse.Shared.Messages;
using MassTransit;
using System.Text.Json;

namespace FreeCourse.Services.LogAPI.Consumers;

public class LogEventConsumer : IConsumer<LogEvent>
{
    private readonly LoggerServiceBase _loggerServiceBase;

    public LogEventConsumer(LoggerServiceBase loggerServiceBase)
    {
        _loggerServiceBase = loggerServiceBase;
    }

    public Task Consume(ConsumeContext<LogEvent> context)
    {
        return Task.Run(() =>
        {
            string logMessage = JsonSerializer.Serialize(context.Message);
            _loggerServiceBase.Info(logMessage);
        });
    }
}
