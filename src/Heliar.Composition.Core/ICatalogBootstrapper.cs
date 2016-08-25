using System;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Registration;
using System.Reflection;

namespace Heliar.Composition.Core
{
	/// <summary>
	/// Represents a type that knows how to bootstrap a MEF <see cref="AggregateCatalog"/> using the Heliar Composition Framework. Implement this interface if
	/// you are going to build a <see cref="CompositionContainer"/> elsewhere.
	/// </summary>
	public interface ICatalogBootstrapper : IBootstrapperBehavior
	{
		/// <summary>
		/// Bootstraps an <see cref="AggregateCatalog" /> by convention and/or specified params.
		/// </summary>
		/// <param name="assemblies">Additional assemblies that contain desired dependencies.</param>
		/// <returns>An <see cref="AggregateCatalog" /> containing dependencies.</returns>
		AggregateCatalog Bootstrap(Action<RegistrationBuilder> registrationFinisher = null, params Assembly[] assemblies);
	}
}