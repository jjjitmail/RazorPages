using Dto;
using Microsoft.Extensions.Caching.Memory;

namespace ServiceLib.Interfaces
{
    public interface IValidationService
    {
        Task<bool> ValidateForDeleteAsync<TSource>(
                    TSource source,
                    IMemoryCache _memoryCache) where TSource : Todo;

        Task<bool> ValidateForCreateAsync<TSource>(
                    TSource source,
                    IMemoryCache _memoryCache) where TSource : Todo;

        Task<bool> ValidateForUpdateAsync<TSource>(
                    TSource source,
                    IMemoryCache _memoryCache) where TSource : Todo;
    }
}
