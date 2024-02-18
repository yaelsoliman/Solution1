using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.PipelineBehaviours.Contracts
{
    public interface ICacheRemoval
    {
        public List<string> CacheKeys { get; set; }
    }
}
