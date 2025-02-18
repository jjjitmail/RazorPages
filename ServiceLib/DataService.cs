using Microsoft.Extensions.Caching.Memory;
using ServiceLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLib
{
    public class DataService : IDataService
    {
        public async Task<IList<TSource>> GetListAsync<TSource>(                    
                    IMemoryCache _memoryCache)
        {
            var list = new List<TSource>();

            var cache = _memoryCache.Get(0);
            if (cache != null)
            {
                list = (List<TSource>)cache;
            }

            return list;
        }

        public async Task<bool> UpdateCacheAsync<TSource>(
                    IList<TSource> source,
                    IMemoryCache _memoryCache)
        {
            var cache = await GetListAsync<TSource>(_memoryCache);
            if (cache != null)
            {
                _memoryCache.Set<IList<TSource>>(0, source);
                return true;
            }

            return false;
        }
    }
}

//Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Files\PIMSDB_ZIUT.mdf;Integrated Security=True;Connect Timeout=30;MultipleActiveResultSets=True;App=EntityFramework

//data source=BRULAP01059;initial catalog=PIMSDB_ZIUT;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework
