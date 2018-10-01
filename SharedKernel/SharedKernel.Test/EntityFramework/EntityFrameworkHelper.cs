using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace SharedKernel.Test.EntityFramework
{
    public class EntityFrameworkHelper
    {
        private static IConfigurationRoot _configuration;

        private static void StartConfig()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            _configuration = builder.Build();
        }

        public static DbContext CreateContext()
        {
            StartConfig();

            var opBuilder = new DbContextOptionsBuilder<SharedKernelTestContext>();
            opBuilder.UseSqlServer(_configuration.GetConnectionString("SharedKernel.Test"));
            return new SharedKernelTestContext(opBuilder.Options);
        }

        public static void CreateSchema()
        {
            var context = CreateContext();
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }
    }
}
