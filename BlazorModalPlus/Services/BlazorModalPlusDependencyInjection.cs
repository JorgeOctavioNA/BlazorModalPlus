using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorModalPlus.Services
{
    public static class BlazorModalPlusDependencyInjection
    {
        /// <summary>
        /// Add the BlazorModalPlus services to the service collection
        /// </summary>
        /// <param name="services">Service Collection that extends</param>
        /// <param name="supportedCultures">Supported Cultures for the Buttons in the BsSimpleConfirmDialog</param>
        /// <param name="defaultCulture">Default culture to use</param>
        /// <returns>ServicesCollection extended with this service</returns>
        public static IServiceCollection AddBlazorModalPlusServices(this IServiceCollection services,
            string[] supportedCultures, string defaultCulture = "en-US")
        {
            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.SetDefaultCulture(defaultCulture)
                       .AddSupportedCultures(supportedCultures)
                       .AddSupportedUICultures(supportedCultures);
            });

            return services;
        }
    }
}
