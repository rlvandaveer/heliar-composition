# heliar-composition
A Managed Extensibility Framework (MEF) based DI framework for all application types (but especially for ASP.NET MVC and WebAPI)

Heliar-Composition is composed of four projects:
* Heliar.Composition.Core - provides a framework for automatically wiring up dependencies across a wide range of project types
* Heliar.Composition.Web - provides base functionality and abstractions for using MEF with MVC and WebAPI
* Heliar.Composition.Mvc - provides a MEF based MVC dependency resolver, FilterAttribute and ModelBinder providers
* Heliar.Composition.WebApi - provides a MEF based WebAPI dependency resolver, inline and route constraint resolvers, and filter attribute and model binder providers

All of these projects are available as Nuget packages: https://www.nuget.org/packages?q=Heliar
