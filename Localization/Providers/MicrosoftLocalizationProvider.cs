﻿/*
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

using Microsoft.Extensions.Localization;

#endregion

namespace Sidub.Platform.Localization.Providers
{

    /// <summary>
    /// Provides localization services using Microsoft's localization infrastructure.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="MicrosoftLocalizationProvider"/> class.
    /// </remarks>
    /// <param name="stringLocalizerFactory">The string localizer factory.</param>
    /// <param name="defaultResources">The default resources.</param>
    public class MicrosoftLocalizationProvider(IStringLocalizerFactory stringLocalizerFactory, IEnumerable<ILocalizationResource> defaultResources) : ILocalizationProvider
    {

        #region Member variables

        private readonly IStringLocalizerFactory _stringLocalizerFactory = stringLocalizerFactory;
        private readonly List<ILocalizationResource> _resources = defaultResources.ToList();

        #endregion

        #region Public methods

        /// <summary>
        /// Gets the localized string for the specified name and culture.
        /// </summary>
        /// <param name="name">The name of the string to localize.</param>
        /// <param name="culture">The culture to use for localization. If null, the current culture is used.</param>
        /// <returns>The localized string, or null if no localized string is found.</returns>
        public Task<string?> GetString(string name, string? culture = null)
        {
            LocalizedString? result = null;

            foreach (var i in _resources)
            {
                var localizer = _stringLocalizerFactory.Create(i.GetType());
                result = localizer.GetString(culture, name);

                if (!result.ResourceNotFound)
                    break;
            }

            if (result == null || result.ResourceNotFound)
                return Task.FromResult<string?>(null);

            return Task.FromResult<string?>(result.Value);
        }

        /// <summary>
        /// Gets the localized string for the specified name and culture, using the specified type.
        /// </summary>
        /// <typeparam name="T">The type to use for localization.</typeparam>
        /// <param name="name">The name of the string to localize.</param>
        /// <param name="culture">The culture to use for localization. If null, the current culture is used.</param>
        /// <returns>The localized string, or null if no localized string is found.</returns>
        public Task<string?> GetString<T>(string name, string? culture = null)
        {
            var localizer = _stringLocalizerFactory.Create(typeof(T));

            var result = localizer.GetString(culture, name);

            return result.ResourceNotFound
                ? Task.FromResult<string?>(null)
                : Task.FromResult<string?>(result.Value);
        }

        #endregion

    }

}
