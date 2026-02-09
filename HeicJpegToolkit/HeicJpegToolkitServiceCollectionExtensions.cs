using HeicJpegToolkit.Helpers.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using System;

namespace HeicJpegToolkit
{
    /// <summary>
    /// ASP.NET Core dependency injection helpers to make it easy to register HeicJpegToolkit.
    /// </summary>
    public static class HeicJpegToolkitServiceCollectionExtensions
    {
        /// <summary>
        /// Registers HeicJpegToolkit-related services and allows configuration of default <see cref="ConversionOptions"/>.
        /// </summary>
        public static IServiceCollection AddHeicJpegToolkit(
            this IServiceCollection services,
            Action<ConversionOptions>? configure = null)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            var options = new ConversionOptions();
            configure?.Invoke(options);

            services.TryAddSingleton(options);
            services.TryAddSingleton<ILogger>(sp =>
            {
                var factory = sp.GetRequiredService<ILoggerFactory>();
                return factory.CreateLogger("HeicJpegToolkit");
            });

            return services;
        }
    }
}

