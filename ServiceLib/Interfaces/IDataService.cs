using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLib.Interfaces
{
    public interface IDataService
    {
        Task<IList<TSource>> GetListAsync<TSource>(
                    IMemoryCache _memoryCache);

        Task<bool> UpdateCacheAsync<TSource>(
                    IList<TSource> source,
                    IMemoryCache _memoryCache);
    }
}
