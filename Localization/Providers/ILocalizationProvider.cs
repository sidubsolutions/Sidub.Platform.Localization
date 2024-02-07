/*
 * Sidub Platform - Localization
 * Copyright (C) 2024 Sidub Solutions. All rights reserved.
 *
 * Dual-licensed under the AGPL v3 or a proprietary license. For details, see
 * https://sidub.ca/licensing or the LICENSE.txt file.
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
