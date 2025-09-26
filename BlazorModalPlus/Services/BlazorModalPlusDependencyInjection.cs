using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorModalPlus.Services
{
    /// <summary>
    /// Extension methods for adding BlazorModalPlus services to the DI container
    /// </summary>
    public static class BlazorModalPlusDependencyInjection
    {
        /// <summary>
        /// Add the BlazorModalPlus services to the service collection
        /// </summary>
        /// <param name="services">Service Collection that extends</param>
        /// <param name="supportedCultures">Supported Cultures for the Buttons in the BsSimpleConfirmDialog</param>
        /// <param name="defaultCulture">Default culture to use</param>
        /// <param name="includeModalService">Whether to register the modal dialog service for programmatic usage</param>
        /// <returns>ServicesCollection extended with this service</returns>
        public static IServiceCollection AddBlazorModalPlusServices(this IServiceCollection services,
            string[] supportedCultures, string defaultCulture = "en-US", bool includeModalService = true)
        {
            if (supportedCultures == null || supportedCultures.Length == 0)
            {
                throw new ArgumentException("Supported cultures cannot be null or empty.", nameof(supportedCultures));
            }

            if (string.IsNullOrWhiteSpace(defaultCulture))
            {
                throw new ArgumentException("Default culture cannot be null or empty.", nameof(defaultCulture));
            }

            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.SetDefaultCulture(defaultCulture)
                       .AddSupportedCultures(supportedCultures)
                       .AddSupportedUICultures(supportedCultures);
            });

            if (includeModalService)
            {
                services.AddScoped<IModalDialogService, ModalDialogService>();
            }

            return services;
        }

        /// <summary>
        /// Add only the basic BlazorModalPlus services without localization
        /// </summary>
        /// <param name="services">Service Collection that extends</param>
        /// <param name="includeModalService">Whether to register the modal dialog service for programmatic usage</param>
        /// <returns>ServicesCollection extended with this service</returns>
        public static IServiceCollection AddBlazorModalPlusBasicServices(this IServiceCollection services, bool includeModalService = true)
        {
            if (includeModalService)
            {
                services.AddScoped<IModalDialogService, ModalDialogService>();
            }

            return services;
        }
    }
}
