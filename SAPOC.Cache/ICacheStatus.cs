using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAPOC.Cache
{
    public interface ICacheStatus
    {
        bool IsCacheEnabled { get; }

        bool IsCacheRunning { get; }
    }
}
