using System;
using System.IO;
using Frisco_automated_shopping.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Frisco_automated_shopping.Configuration
{
    public static class BaseConfiguration
    {
        public static void ConfigureApplication(DeliveryCriteria deliveryCriteria)
        {
            var services = new ServiceCollection();
            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"appsettings.{environmentName}.json", optional: false, reloadOnChange: true)
                .AddUserSecrets<SecureSettings>()
                .AddEnvironmentVariables();

            IConfiguration configuration = builder.Build();

            var appSettings = configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettings);

            var secureSettings = configuration.GetSection("SecureSettings");
            services.Configure<SecureSettings>(secureSettings);

            services.AddTransient<FriscoResolver>();

            var serviceProvider = services.BuildServiceProvider();

            serviceProvider.GetService<FriscoResolver>().InitializeDriver(deliveryCriteria);
        }
    }
}
