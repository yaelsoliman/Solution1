using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.PipelineBehaviours.Contracts
{
    public interface ICacheable
    {
        public string CacheKey { get; set; }
        public bool BypassCache { get; set; }
        public TimeSpan SlidingExpriation { get; set; }
    }
}
