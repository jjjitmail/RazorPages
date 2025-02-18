using Dto;
using Microsoft.Extensions.Caching.Memory;
using ServiceLib.Interfaces;

namespace ServiceLib
{
    public class ValidationService : IValidationService
    {
        public ValidationService() { }

        public async Task<bool> ValidateForCreateAsync<TSource>(
                    TSource source,
                    IMemoryCache _memoryCache) where TSource : Todo
        {
            if (String.IsNullOrWhiteSpace(source.Name))
            {
                return false;
            }
            var cache = await GetListAsync<TSource>(_memoryCache);
            if (cache != null)
            {
                bool result = cache.Count(x => x.Name.ToLower().Trim() == source.Name.ToLower().Trim()) == 0;
                return result;
            }

            return true;
        }

        public async Task<bool> ValidateForUpdateAsync<TSource>(
                    TSource source,
                    IMemoryCache _memoryCache) where TSource : Todo
        {
            if (String.IsNullOrWhiteSpace(source.Name))
            {
                return false;
            }
            var cache = await GetListAsync<TSource>(_memoryCache);
            if (cache != null)
            {
                bool result = cache.Count(x => x.Name.ToLower().Trim() == source.Name.ToLower().Trim()
                                        && !x.Id.Equals(source.Id)) == 0;
                return result;
            }

            return true;
        }

        public async Task<bool> ValidateForDeleteAsync<TSource>(
                    TSource source,
                    IMemoryCache _memoryCache) where TSource : Todo
        {
            var cache = await GetListAsync<TSource>(_memoryCache);
            if (cache != null)
            {
                bool isValid = cache.Any(x => x.Id.Equals(source.Id) && x.Status == Dto.Enum.StatusType.completed);
                return isValid;
            }

            return false;
        }

        private async Task<IList<TSource>> GetListAsync<TSource>(
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
    }
}
