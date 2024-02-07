/*
 * Sidub Platform - Localization
 * Copyright (C) 2024 Sidub Solutions. All rights reserved.
 *
 * Dual-licensed under the AGPL v3 or a proprietary license. For details, see
 * https://sidub.ca/licensing or the LICENSE.txt file.
 */

#region Imports

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Sidub.Platform.Localization.Providers;
using Sidub.Platform.Localization.Services;

#endregion

namespace Sidub.Platform.Localization
{

    /// <summary>
    /// Static helper class providing IServiceCollection extensions.
    /// </summary>
    public static class ServiceCollectionExtension
    {

        #region Extension methods

        /// <summary>
        /// Registers the Sidub Platform localization framework.
        /// </summary>
        /// <param name="services">IServiceCollection extension.</param>
        /// <returns>IServiceCollection result.</returns>
        public static IServiceCollection AddSidubLocalization(
            this IServiceCollection services)
        {
            services.AddLogging();
            services.AddLocalization();
            services.TryAddEnumerable(ServiceDescriptor.Transient<ILocalizationProvider, MicrosoftLocalizationProvider>());
            services.TryAddTransient<ILocalizationService, LocalizationService>();

            return services;
        }

        /// <summary>
        /// Registers a global resource for use by the Sidub Platform localization framework.
        /// </summary>
        /// <param name="services">IServiceCollection extension.</param>
        /// <param name="parser">Filter parser configuration type.</param>
        /// <returns>IServiceCollection result.</returns>
        public static IServiceCollection AddSidubLocalizationResource<TLocalizationResource>(this IServiceCollection services) where TLocalizationResource : class, ILocalizationResource
        {
            services.TryAddEnumerable(ServiceDescriptor.Transient<ILocalizationResource, TLocalizationResource>());

            return services;
        }

        #endregion

    }
}
