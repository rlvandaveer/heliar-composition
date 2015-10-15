using System.ComponentModel.Composition.Hosting;

namespace Heliar.Composition.Core
{
	/// <summary>
	/// Represents a type that knows how to register the dependencies within a library.
	/// </summary>
	public interface ILibraryDependencyRegistrar
	{
		/// <summary>
		/// Bootstraps the dependencies within this library and adds them to the specified catalog.
		/// </summary>
		/// <param name="catalog">The catalog to add dependencies to.</param>
		void Register(AggregateCatalog catalog);
	}
}
