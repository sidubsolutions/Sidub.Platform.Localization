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

using Microsoft.Extensions.Logging;
using Sidub.Platform.Localization.Providers;

#endregion

namespace Sidub.Platform.Localization.Services
{

    /// <summary>
    /// Service for handling localization operations.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="LocalizationService"/> class.
    /// </remarks>
    /// <param name="localizationProviders">The localization providers.</param>
    /// <param name="loggerFactory">The logger factory.</param>
    public class LocalizationService(IEnumerable<ILocalizationProvider> localizationProviders, ILoggerFactory loggerFactory) : ILocalizationService
    {

        #region Member variables

        private readonly IEnumerable<ILocalizationProvider> _localizationProviders = localizationProviders;
        private readonly ILogger _logger = loggerFactory.CreateLogger<LocalizationService>();

        #endregion

        #region Public methods

        /// <summary>
        /// Gets the localized string for the specified name and culture.
        /// </summary>
        /// <param name="name">The name of the string to localize.</param>
        /// <param name="culture">The culture to use for localization. If null, the current culture is used.</param>
        /// <returns>The localized string, or the name if no localized string is found.</returns>
        public async Task<string> GetString(string name, string? culture = null)
        {
            try
            {
                foreach (var provider in _localizationProviders)
                {
                    var result = await provider.GetString(name, culture);

                    if (result is not null)
                        return result;
                }
            }
            catch (Exception ex)
            {
                // localization must be a non-destructive operation...
                _logger.LogWarning(ex, "Failed to get localized string for key '{name}' and culture '{culture}'.", name, culture);
            }

            // return key if no resource value is found... this allows us to define the default labels in code and apply localization
            //  later without having to define a resource file for every label...
            return name;
        }

        /// <summary>
        /// Gets the localized string for the specified name and culture, using the specified type.
        /// </summary>
        /// <typeparam name="T">The type to use for localization.</typeparam>
        /// <param name="name">The name of the string to localize.</param>
        /// <param name="culture">The culture to use for localization. If null, the current culture is used.</param>
        /// <returns>The localized string, or the name if no localized string is found.</returns>
        public async Task<string> GetString<T>(string name, string? culture = null)
        {
            try
            {
                foreach (var provider in _localizationProviders)
                {
                    var result = await provider.GetString<T>(name, culture);

                    if (result is not null)
                        return result;
                }
            }
            catch (Exception ex)
            {
                // localization must be a non-destructive operation...
                _logger.LogWarning(ex, "Failed to get localized string for key '{name}' and culture '{culture}'.", name, culture);
            }

            // return key if no resource value is found... this allows us to define the default labels in code and apply localization
            //  later without having to define a resource file for every label...
            return name;
        }

        #endregion

    }

}
