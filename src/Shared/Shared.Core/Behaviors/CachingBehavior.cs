using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace Shared.Core.Behaviors
{
    public interface ICacheableRequest
    {
        public string CacheKey { get; }
        public TimeSpan ExpirationTime { get; }
    }

    public class CachingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : ICacheableRequest
    {
        private readonly IMemoryCache _cache;

        public CachingBehavior(IMemoryCache cache)
        {
            _cache = cache;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var cachedResponse = _cache.Get<TResponse>(request.CacheKey);
            if (cachedResponse != null)
            {
                return cachedResponse;
            }

            var response = await next();
            _cache.Set(request.CacheKey, response, request.ExpirationTime);

            return response;
        }
    }
}