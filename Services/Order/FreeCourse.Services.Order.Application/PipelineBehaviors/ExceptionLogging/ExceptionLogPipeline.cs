using FreeCourse.Shared.CrossCuttingConcerns;
using FreeCourse.Shared.Messages;
using MassTransit;
using MediatR;

namespace FreeCourse.Services.Order.Application.PipelineBehaviors.ExceptionLogging
{
    public sealed class ExceptionLogPipeline<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public ExceptionLogPipeline(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            try
            {
                return await next();
            }
            catch (Exception ex)
            {
                ExceptionLogEvent logDetailWithException = GetExceptionMethodDetail(request, ex);
                await _publishEndpoint.Publish<ExceptionLogEvent>(logDetailWithException);
                throw;
            }
        }

        private ExceptionLogEvent GetExceptionMethodDetail(TRequest request, Exception e)
        {
            List<LogParameter> logParameters = request.GetType().GetProperties().Select(r => new LogParameter
            {
                Name = r.Name,
                Type = r.GetType().ToString(),
                Value = r.GetValue(request)
            }).ToList();

            ExceptionLogEvent logDetailWithException = new()
            {
                MethodName = request.GetType().FullName,
                Parameters = logParameters,
                ExceptionMessage = e.Message
            };

            return logDetailWithException;
        }
    }
}
