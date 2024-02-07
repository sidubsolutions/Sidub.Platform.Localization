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