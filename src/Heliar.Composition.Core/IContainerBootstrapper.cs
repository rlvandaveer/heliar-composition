using System.ComponentModel.Composition.Hosting;
using System.Reflection;

namespace Heliar.Composition.Core
{
	/// <summary>
	/// Represents a type that knows how to bootstrap a MEF <see cref="CompositionContainer"/> using the Heliar Composition Framework.
	/// </summary>
	public interface IContainerBootstrapper : IBootstrapperBehavior
	{
		/// <summary>
		/// Wires up a <see cref="CompositionContainer"/>.
		/// </summary>
		/// <param name="assemblies">The assemblies that contain the desired dependencies.</param>
		/// <returns>A <see cref="CompositionContainer"/> that can resolve dependencies.</returns>
		CompositionContainer Bootstrap(params Assembly[] assemblies);
	}
}