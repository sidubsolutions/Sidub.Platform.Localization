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

namespace Sidub.Platform.Localization.Providers
{

    /// <summary>
    /// Represents a localization provider that retrieves localized strings.
    /// </summary>
    public interface ILocalizationProvider
    {

        #region Interface methods

        /// <summary>
        /// Gets the localized string for the specified name and culture.
        /// </summary>
        /// <param name="name">The name of the string to retrieve.</param>
        /// <param name="culture">The culture of the localized string. If not specified, the default culture will be used.</param>
        /// <returns>The localized string.</returns>
        Task<string?> GetString(string name, string? culture = null);

        /// <summary>
        /// Gets the localized string for the specified name and culture, with support for string formatting.
        /// </summary>
        /// <typeparam name="T">The type of the object to format the string with.</typeparam>
        /// <param name="name">The name of the string to retrieve.</param>
        /// <param name="culture">The culture of the localized string. If not specified, the default culture will be used.</param>
        /// <returns>The formatted localized string.</returns>
        Task<string?> GetString<T>(string name, string? culture = null);

        #endregion

    }

}
