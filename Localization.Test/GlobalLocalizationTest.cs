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

using Localization.Test.Resources;
using Microsoft.Extensions.DependencyInjection;
using Sidub.Platform.Localization;
using Sidub.Platform.Localization.Services;

namespace Localization.Test
{

    [TestClass]
    public class GlobalLocalizationTest
    {

        private readonly ILocalizationService _localizationService;

        public GlobalLocalizationTest()
        {
            // initialize dependency injection environment...
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddSidubLocalization();
            serviceCollection.AddSidubLocalizationResource<GlobalTestResource>();

            var serviceProvider = serviceCollection.BuildServiceProvider();
            _localizationService = serviceProvider.GetRequiredService<ILocalizationService>();
        }

        [TestMethod]
        public async Task GlobalLocalizationTest_ExplicitKey()
        {
            var key = "TestResourceString01";
            var expectedEnglish = "This is a test resource string based on explicit key definition.";
            var expectedFrench = "Il s’agit d’une chaîne de ressource de test basée sur une définition de clé explicite.";

            var actualEnglish = await _localizationService.GetString(key, "en-US");
            var actualFrench = await _localizationService.GetString(key, "fr-CA");

            Assert.AreEqual(expectedEnglish, actualEnglish);
            Assert.AreEqual(expectedFrench, actualFrench);
        }

        [TestMethod]
        public async Task GlobalLocalizationTest_ImplicitKey()
        {
            var key = "This is a test resource string based on implicit definition.";
            var expectedEnglish = key;
            var expectedFrench = "Il s’agit d’une chaîne de ressource de test basée sur une définition implicite.";

            var actualEnglish = await _localizationService.GetString(key, "en-US");
            var actualFrench = await _localizationService.GetString(key, "fr-CA");

            Assert.AreEqual(expectedEnglish, actualEnglish);
            Assert.AreEqual(expectedFrench, actualFrench);
        }

    }

}