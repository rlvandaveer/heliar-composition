using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Registration;
using System.Diagnostics.CodeAnalysis;

using Heliar.Composition.Core;

using Samples.Common;

namespace Samples.ConsoleApp
{
	/// <summary>
	/// This class is the Console's DependencyRegistrar.
	/// </summary>
	[ExcludeFromCodeCoverage]
	public class ConsoleDependencyRegistrar : IApplicationDependencyRegistrar
	{
		/// <summary>
		/// Registers the dependencies within this application.
		/// </summary>
		/// <param name="registrations">The registrations.</param>
		public void Register(RegistrationBuilder registrations, AggregateCatalog catalog)
		{
			registrations.ForTypesDerivedFrom<ILogger>()
				.SetCreationPolicy(CreationPolicy.Shared)
				.ExportInterfaces()
				.Export();
		}
	}
}