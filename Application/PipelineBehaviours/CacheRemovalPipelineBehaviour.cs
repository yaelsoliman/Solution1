using Application.Models;
using Application.PipelineBehaviours.Contracts;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.PipelineBehaviours
{
    public class CacheRemovalPipelineBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>, ICacheRemoval
    {
        private readonly IDistributedCache _cache;
        private readonly CacheSettings _cacheSetting;

        public CacheRemovalPipelineBehaviour(IDistributedCache cache, IOptions<CacheSettings> cacheSetting)
        {
            _cache = cache;
            _cacheSetting = cacheSetting.Value;
        }
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            TResponse response = await next();
            foreach (var key in request.CacheKeys)
            {
                string chachKey = $"{_cacheSetting.ApplicationName}:{key}";
                var cacheResponse = await _cache.GetAsync(chachKey, cancellationToken);
                if (cacheResponse != null)
                {
                    await _cache.RemoveAsync(chachKey, cancellationToken);
                }
            }
         
            return response;
        }
    }
}
