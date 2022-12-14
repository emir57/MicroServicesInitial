using MediatR;

namespace FreeCourse.Services.Order.Application.PipelineBehaviors
{
    public abstract class BasePipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        protected virtual void OnBefore(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next) { }
        protected virtual void OnAfter(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next) { }
        protected virtual void OnException(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next, Exception e) { }
        protected virtual void OnSuccess(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next) { }

        public virtual async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            bool isSuccess = true;
            OnBefore(request, cancellationToken, next);
            try
            {
                return await next();
            }
            catch (Exception e)
            {
                isSuccess = false;
                OnException(request, cancellationToken, next, e);
                throw;
            }
            finally
            {
                if (isSuccess)
                    OnSuccess(request, cancellationToken, next);
                OnAfter(request, cancellationToken, next);
            }
        }
    }
}
