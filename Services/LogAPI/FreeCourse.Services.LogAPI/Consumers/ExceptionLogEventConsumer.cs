using FreeCourse.Shared.Messages;
using MassTransit;

namespace FreeCourse.Services.LogAPI.Consumers;

public class ExceptionLogEventConsumer : IConsumer<ExceptionLogEvent>
{
    public Task Consume(ConsumeContext<ExceptionLogEvent> context)
    {
        throw new NotImplementedException();
    }
}
