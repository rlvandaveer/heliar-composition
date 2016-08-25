using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Registration;

namespace Heliar.Composition.Core
{
	/// <summary>
	/// Represents a type that knows how to register the dependencies within a library.
	/// </summary>
	public interface ILibraryDependencyRegistrar
	{
		/// <summary>
		/// Bootstraps the dependencies within this library.
		/// </summary>
		/// <param name="registrations">The registrations.</param>
		void Register(RegistrationBuilder registrations);
	}
}
