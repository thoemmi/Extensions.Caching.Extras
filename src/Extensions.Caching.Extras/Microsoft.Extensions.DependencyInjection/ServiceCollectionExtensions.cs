using Extensions.Caching.Extras;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Provides methods to extend the behavior of <see cref="IServiceCollection" />.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds <see cref="IMemoryCachePartition{TPartition}"/> to the service collection.
        /// </summary>
        /// <param name="services">The <see cref="T:Microsoft.Extensions.DependencyInjection.IServiceCollection" /> to add the service to.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        public static IServiceCollection AddMemoryCachePartitions(this IServiceCollection services)
        {
            services.AddTransient(typeof(IMemoryCachePartition<>), typeof(MemoryCachePartition<>));
            return services;
        }
    }
}
