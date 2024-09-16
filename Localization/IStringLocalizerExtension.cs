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
