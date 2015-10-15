using System.ComponentModel.Composition.Hosting;
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
		/// Wires up a <see cref="ComposablePartCatalog"/>.
		/// </summary>
		/// <param name="assemblies">The assemblies that contain the desired dependencies.</param>
		/// <returns>An <see cref="AggregateCatalog"/> containing dependencies.</returns>
		AggregateCatalog Bootstrap(params Assembly[] assemblies);
	}
}