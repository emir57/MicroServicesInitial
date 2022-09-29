using FreeCourse.Shared.Messages;
using MassTransit;

namespace FreeCourse.Services.LogAPI.Consumers;

public class LogEventConsumer : IConsumer<LogEvent>
{
    public Task Consume(ConsumeContext<LogEvent> context)
    {
        throw new NotImplementedException();
    }
}
