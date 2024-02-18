using Application.Models;
using Application.PipelineBehaviours.Contracts;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.PipelineBehaviours
{
    public class CachePipelineBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>,ICacheable
    {
        private readonly IDistributedCache _cache;
        private readonly CacheSettings _cacheSettings;

        public CachePipelineBehaviour(IOptions<CacheSettings> cacheSettings, IDistributedCache cache)
        {
            _cacheSettings = cacheSettings.Value;
            _cache = cache;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if(request.BypassCache) return await next();
            
            TResponse response;
            string cacheKey=$"{_cacheSettings.ApplicationName}:{request.CacheKey}";
            var cacheResponse=await _cache.GetAsync(cacheKey, cancellationToken);
            if(cacheResponse != null)
            {
                response=JsonConvert.DeserializeObject<TResponse>(Encoding.Default.GetString(cacheResponse));
            }
            else
            {
                //Get the response and write to cache
                response = await GetResponseAndWriteToCacheAsync();
            }
            return response;
           
            async Task<TResponse> GetResponseAndWriteToCacheAsync()
            {
                response =await next();

                if(response != null)
                {
                    var slidingExpiration = request?.SlidingExpriation == TimeSpan.Zero?
                        TimeSpan.FromMinutes(_cacheSettings.SlidingExpiration)
                        :request.SlidingExpriation;

                    var cacheOptions = new DistributedCacheEntryOptions
                    {
                        SlidingExpiration = slidingExpiration,
                        AbsoluteExpiration=DateTime.Now.AddDays(1)
                    };
                    var serializedData=Encoding.Default
                        .GetBytes(JsonConvert.SerializeObject(response,
                        Formatting.Indented,
                        new JsonSerializerSettings()
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                        }));
                    await _cache.SetAsync(cacheKey, serializedData,cacheOptions,cancellationToken);
                }

                return response;
            }
        }
    }
}
