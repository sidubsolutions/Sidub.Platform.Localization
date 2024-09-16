# Sidub Platform - Localization

This repository contains the localization module for the Sidub Platform. It provides a
flexible and extensible localization system that can be easily integrated into any .NET
application.

## Main Components

### LocalizationService

The `LocalizationService` is the main entry point for localization operations in the
application. It uses a collection of `ILocalizationProvider` instances to retrieve
localized strings based on a key and culture. If a localized string is not found, it
returns the key as a fallback. This allows you to define default labels in code and apply
localization later without having to define a resource file for every label.

### MicrosoftLocalizationProvider

The `MicrosoftLocalizationProvider` is an implementation of `ILocalizationProvider` that
uses Microsoft's localization infrastructure to provide localized strings. It uses an
`IStringLocalizerFactory` to create `IStringLocalizer` instances for each type of
resource.

### ServiceCollectionExtension

The `ServiceCollectionExtension` provides extension methods for `IServiceCollection` to
register the localization services and providers with the dependency injection container.
The `AddSidubLocalization` method registers the `LocalizationService` and
`MicrosoftLocalizationProvider` with the container.

## Usage

To use the localization system, first register it with the dependency injection container
in your `Startup` class:

```csharp
public void ConfigureServices(IServiceCollection services)
{
	services.AddSidubLocalization();
}
```

The `AddSidubLocalization` method registers the localization services within the 
container. The `AddSidubLocalizationResource` method is used to register global resources.

### Global Resources

Global resources are registered using the `AddSidubLocalizationResource` method. This
method takes a type parameter that represents the resource class. The resource class must
implement the `ILocalizationResource` interface. Here's an example of registering the
`GlobalTestResource` as a global resource:

```charp
services.AddSidubLocalizationResource<GlobalTestResource>();
```

### Typed Resources

Typed resources are not registered within the container; rather they are referenced when
retrieving localized strings. To use a typed resource, create a class that implements the
`ILocalizationResource` interface. Then, inject the `ILocalizationService` into your
classes and use it to retrieve localized strings - be sure to utilize the 
'GetString<TLocalizationResource>' method,providing the localization resources class as a 
type parameter.


### Retrieving Localized Strings
Inject the `LocalizationService` into your classes and use it to retrieve localized
strings:

```csharp
public class MyClass { private readonly ILocalizationService _localizationService;
    public MyClass(ILocalizationService localizationService)
    {
        _localizationService = localizationService;
    }

    public void MyMethod()
    {
        // Retrieves a localized string for the current culture...
        string localizedString = _localizationService.GetString("MyKey");

        // Retrieves a localized string for a specific culture...
        string localizedString = _localizationService.GetString("MyKey", "en-CA");

        // Retrieves a localized string for a specific culture using a typed resource...
        string localizedString = _localizationService.GetString<TypedTestResource>("MyKey", "en-CA");
    }
}
```

## Considerations
### Implicit vs. Explicit Resource Keys
When defining resource keys, there are two general approaches: implicit and explicit. Explicit resource
strings are enum-type keys such as "MyResourceKey" and display equivalents are defined in every language / 
culture, even the one being developed in locally.

Implicit resource strings mean using the full text label / description as the resource key. This allows
developers to expedite development by not having to define resource keys and strings for every label during
development. Rather, resources may be added later for specific languages / cultures as needed by using the
full text label keys. This does not work in all scenarios, but is a useful approach in many cases.

### Resource Culture Fallback
The system will attempt to retrieve a localized string for the requested culture. If a localized string is not
found for the specific culture, the system will attempt to retrieve a localized string for the parent culture /
less granular match.

For example if we request a resource string for "en-CA" and it is not found, the system will attempt to retrieve
the resource string for "en" and so on until a match is found. If no match is found, the system will return the
resource key as a fallback. This allows for a more granular approach to localization, where only the specific
differences between cultures need to be defined.

## License

This project is dual-licensed under the AGPL v3 or a proprietary license. For details, see [https://sidub.ca/licensing](https://sidub.ca/licensing) or the LICENSE.txt file.
