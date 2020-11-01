using FluentAssertions;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Extensions.Caching.Extras.Tests
{
    public class DependencyInjectionTests
    {
        [Fact]
        public void Can_inject_IPartitionedMemoryCache()
        {
            var services = new ServiceCollection();
            services.AddMemoryCache();
            services.AddMemoryCachePartitions();
            services.AddTransient<TestService>();
            var serviceProvider = services.BuildServiceProvider();

            var testService = serviceProvider.GetService<TestService>();

            testService.CachePartition.Should().BeOfType<MemoryCachePartition<TestService>>();

            testService.CachePartition.Set("key", "value");

            var cacheValue = serviceProvider.GetService<IMemoryCache>().Partition<TestService>().Get("key");

            cacheValue.Should().BeOfType<string>().Which.Should().Be("value");
        }

        public class TestService
        {
            public TestService(IMemoryCachePartition<TestService> cachePartition)
            {
                CachePartition = cachePartition;
            }

            public IMemoryCachePartition<TestService> CachePartition { get; private set; }
        }
    }
}