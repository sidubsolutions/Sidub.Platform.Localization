/*
 * Sidub Platform - Localization
 * Copyright (C) 2024 Sidub Solutions. All rights reserved.
 *
 * Dual-licensed under the AGPL v3 or a proprietary license. For details, see
 * https://sidub.ca/licensing or the LICENSE.txt file.
 */

#region Imports

using Microsoft.Extensions.Localization;
using System.Globalization;

#endregion

namespace Sidub.Platform.Localization
{

    /// <summary>
    /// Extension methods for <see cref="IStringLocalizer"/>.
    /// </summary>
    public static class IStringLocalizerExtension
    {

        #region Extension methods

        /// <summary>
        /// Gets the localized string for the specified name and culture.
        /// </summary>
        /// <param name="stringLocalizer">The <see cref="IStringLocalizer"/> instance.</param>
        /// <param name="culture">The culture for which to retrieve the localized string. If null or empty, the current UI culture is used.</param>
        /// <param name="name">The name of the localized string.</param>
        /// <param name="arguments">The arguments to format the localized string.</param>
        /// <returns>The localized string.</returns>
        public static LocalizedString GetString(this IStringLocalizer stringLocalizer, string? culture, string name, params object[] arguments)
        {
            CultureInfo requestedCulture = string.IsNullOrEmpty(culture) ? CultureInfo.CurrentUICulture : CultureInfo.GetCultureInfo(culture);

            CultureInfo cultureOriginal = CultureInfo.CurrentCulture;
            CultureInfo cultureUIOriginal = CultureInfo.CurrentUICulture;

            try
            {
                CultureInfo.CurrentCulture = requestedCulture;
                CultureInfo.CurrentUICulture = requestedCulture;
                return stringLocalizer.GetString(name, arguments);
            }
            finally
            {
                CultureInfo.CurrentCulture = cultureOriginal;
                CultureInfo.CurrentUICulture = cultureUIOriginal;
            }
        }

        #endregion

    }

}
