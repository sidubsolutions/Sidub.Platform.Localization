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
    public class TypedLocalizationTest
    {

        private readonly ILocalizationService _localizationService;

        public TypedLocalizationTest()
        {
            // initialize dependency injection environment...
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddSidubLocalization();
            serviceCollection.AddSidubLocalizationResource<GlobalTestResource>();

            var serviceProvider = serviceCollection.BuildServiceProvider();
            _localizationService = serviceProvider.GetRequiredService<ILocalizationService>();
        }

        [TestMethod]
        public async Task TypedLocalizationTest_ExplicitKey()
        {
            var key = "TestResourceString01";
            var expectedAmerican = "This is an explicit key test resource string under a typed resource.";
            var expectedCanadian = "This is an explicit key test resource string under a typed resource, eh!";
            var expectedFrench = "Il s’agit d’une chaîne de ressource de test de clé explicite sous une ressource typée.";

            var actualAmerican = await _localizationService.GetString<TypedTestResource>(key, "en-US");
            var actualCanadian = await _localizationService.GetString<TypedTestResource>(key, "en-CA");
            var actualFrench = await _localizationService.GetString<TypedTestResource>(key, "fr-CA");

            Assert.AreEqual(expectedAmerican, actualAmerican);
            Assert.AreEqual(expectedCanadian, actualCanadian);
            Assert.AreEqual(expectedFrench, actualFrench);
        }

        [TestMethod]
        public async Task TypedLocalizationTest_ImplicitKey()
        {
            var key = "This is an implicit key test resource string under a typed resource.";
            var expectedAmerican = key;
            var expectedCanadian = "This is an implicit key test resource string under a typed resource, eh!";
            var expectedFrench = "Il s’agit d’une chaîne de ressource de test de clé implicite sous une ressource typée.";

            var actualAmerican = await _localizationService.GetString<TypedTestResource>(key, "en-US");
            var actualCanadian = await _localizationService.GetString<TypedTestResource>(key, "en-CA");
            var actualFrench = await _localizationService.GetString<TypedTestResource>(key, "fr-CA");

            Assert.AreEqual(expectedAmerican, actualAmerican);
            Assert.AreEqual(expectedCanadian, actualCanadian);
            Assert.AreEqual(expectedFrench, actualFrench);
        }

    }

}