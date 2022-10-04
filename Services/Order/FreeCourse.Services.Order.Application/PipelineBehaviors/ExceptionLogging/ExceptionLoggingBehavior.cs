using FreeCourse.Shared.CrossCuttingConcerns;
using FreeCourse.Shared.Messages;
using MassTransit;
using MediatR;

namespace FreeCourse.Services.Order.Application.PipelineBehaviors.ExceptionLogging
{
    public sealed class ExceptionLoggingBehavior<TRequest, TResponse> : BasePipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public ExceptionLoggingBehavior(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        protected async override void OnException(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next, Exception e)
        {
            ExceptionLogEvent logDetailWithException = GetExceptionMethodDetail(request, e);
            await _publishEndpoint.Publish<ExceptionLogEvent>(logDetailWithException);
        }

        private ExceptionLogEvent GetExceptionMethodDetail(TRequest request, Exception e)
        {
            ExceptionLogEvent logDetailWithException = new()
            {
                MethodName = request.GetType().FullName,
                Parameters = getParameters(request),
                ExceptionMessage = e.Message
            };

            return logDetailWithException;
        }

        private List<LogParameter> getParameters(TRequest request)
        {
            return request.GetType().GetProperties().Select(r => new LogParameter
            {
                Name = r.Name,
                Type = r.GetType().ToString(),
                Value = r.GetValue(request)
            }).ToList();
        }
    }
}
