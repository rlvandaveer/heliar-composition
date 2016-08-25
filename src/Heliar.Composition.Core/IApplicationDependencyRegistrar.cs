using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.ComponentModel.Composition.Registration;
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
		/// Registers the dependencies within this application.
		/// </summary>
		/// <param name="registrations">The registrations.</param>
		void Register(RegistrationBuilder registrations);
	}
}