using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Reflection;

namespace Heliar.Composition.Core
{
	/// <summary>
	/// Represents a type that knows how to register dependencies within an application. Note that an implementing type will automatically
	/// be composed last by the framework.
	/// </summary>
	public interface IApplicationDependencyRegistrar
	{
		/// <summary>
		/// Registers the dependencies within this application and adds them to the specified catalog.
		/// </summary>
		/// <param name="catalog">The catalog to add dependencies to.</param>
		void Register(AggregateCatalog catalog);
	}
}