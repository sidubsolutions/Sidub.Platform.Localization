/*
 * Sidub Platform - Localization
 * Copyright (C) 2024 Sidub Inc.
 * All rights reserved.
 *
 * This file is part of Sidub Platform - Localization (the "Product").
 *
 * The Product is dual-licensed under:
 * 1. The GNU Affero General Public License version 3 (AGPLv3)
 * 2. Sidub Inc.'s Proprietary Software License Agreement (PSLA)
 *
 * You may choose to use, redistribute, and/or modify the Product under
 * the terms of either license.
 *
 * The Product is provided "AS IS" and "AS AVAILABLE," without any
 * warranties or conditions of any kind, either express or implied, including
 * but not limited to implied warranties or conditions of merchantability and
 * fitness for a particular purpose. See the applicable license for more
 * details.
 *
 * See the LICENSE.txt file for detailed license terms and conditions or
 * visit https://sidub.ca/licensing for a copy of the license texts.
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
