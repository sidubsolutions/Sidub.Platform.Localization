/*
 * Sidub Platform - Localization
 * Copyright (C) 2024 Sidub Solutions. All rights reserved.
 *
 * Dual-licensed under the AGPL v3 or a proprietary license. For details, see
 * https://sidub.ca/licensing or the LICENSE.txt file.
 */

namespace Sidub.Platform.Localization.Services
{

    /// <summary>
    /// Interface for localization services.
    /// </summary>
    public interface ILocalizationService
    {

        #region Interface methods

        /// <summary>
        /// Gets the localized string for the specified name and culture.
        /// </summary>
        /// <param name="name">The name of the string to localize.</param>
        /// <param name="culture">The culture to use for localization.</param>
        /// <returns>The localized string.</returns>
        Task<string> GetString(string name, string? culture = null);

        /// <summary>
        /// Gets the localized string for the specified name and culture, using the specified type.
        /// </summary>
        /// <typeparam name="T">The type to use for localization.</typeparam>
        /// <param name="name">The name of the string to localize.</param>
        /// <param name="culture">The culture to use for localization.</param>
        /// <returns>The localized string.</returns>
        Task<string> GetString<T>(string name, string? culture = null);

        #endregion

    }

}
