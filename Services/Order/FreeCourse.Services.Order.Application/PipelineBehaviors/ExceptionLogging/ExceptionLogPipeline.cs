using FreeCourse.Shared.CrossCuttingConcerns;
using FreeCourse.Shared.CrossCuttingConcerns.Serilog;
using MassTransit;
using MediatR;
using System.Text.Json;

namespace FreeCourse.Services.Order.Application.PipelineBehaviors.ExceptionLogging
{
    public sealed class ExceptionLogPipeline<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly ISendEndpointProvider _sendEndpointProvider;

        public ExceptionLogPipeline(ISendEndpointProvider sendEndpointProvider)
        {
            _sendEndpointProvider = sendEndpointProvider;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            try
            {
                return await next();
            }
            catch (Exception ex)
            {
                LogDetailWithException logDetailWithException = GetExceptionMethodDetail(request, ex);
                await _sendEndpointProvider.Send(logDetailWithException);
                throw;
            }
        }

        private LogDetailWithException GetExceptionMethodDetail(TRequest request, Exception e)
        {
            List<LogParameter> logParameters = request.GetType().GetProperties().Select(r => new LogParameter
            {
                Name = r.Name,
                Type = r.GetType().ToString(),
                Value = r
            }).ToList();

            LogDetailWithException logDetailWithException = new()
            {
                MethodName = request.GetType().FullName,
                Parameters = logParameters,
                ExceptionMessage = e.Message
            };

            return logDetailWithException;
        }
    }
}
