using System;
using Microsoft.Extensions.Caching.Memory;

namespace Extensions.Caching.Extras
{
    /// <summary>
    /// A partition over an <see cref="IMemoryCache"/> for a specific typed partition.
    /// </summary>
    /// <typeparam name="TPartition">The type of the partition.</typeparam>
    public interface IMemoryCachePartition<TPartition> : IMemoryCache
    {
    }

    /// <summary>
    /// A partition over an <see cref="IMemoryCache"/> for a specific typed partition.
    /// </summary>
    /// <typeparam name="TPartition">The type of the partition.</typeparam>
    public class MemoryCachePartition<TPartition> : IMemoryCachePartition<TPartition>
    {
        private readonly IMemoryCache _cache;

        /// <summary>
        /// Initializes a new instance of <see cref="MemoryCachePartition{TPartition}"/>.
        /// </summary>
        /// <param name="cache">The cache over which to create a partition.</param>
        public MemoryCachePartition(IMemoryCache cache)
        {
            if (cache == null)
                throw new ArgumentNullException(nameof(cache));

            _cache = new MemoryCachePartition(cache, typeof(TPartition));
        }

        /// <inheritdoc />
        public void Dispose() => _cache.Dispose();

        /// <inheritdoc />
        public bool TryGetValue(object key, out object value) => _cache.TryGetValue(key, out value);

        /// <inheritdoc />
        public ICacheEntry CreateEntry(object key) => _cache.CreateEntry(key);

        /// <inheritdoc />
        public void Remove(object key) => _cache.Remove(key);
    }
}
