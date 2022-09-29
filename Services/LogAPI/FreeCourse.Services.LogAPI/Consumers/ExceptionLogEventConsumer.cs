using FreeCourse.Shared.CrossCuttingConcerns.Serilog;
using FreeCourse.Shared.Messages;
using MassTransit;
using System.Text.Json;

namespace FreeCourse.Services.LogAPI.Consumers;

public class ExceptionLogEventConsumer : IConsumer<ExceptionLogEvent>
{
    private readonly LoggerServiceBase _loggerServiceBase;

    public ExceptionLogEventConsumer(LoggerServiceBase loggerServiceBase)
    {
        _loggerServiceBase = loggerServiceBase;
    }

    public Task Consume(ConsumeContext<ExceptionLogEvent> context)
    {
        return Task.Run(() =>
        {
            string logMessage = JsonSerializer.Serialize(context.Message);
            _loggerServiceBase.Error(logMessage);
        });
    }
}
