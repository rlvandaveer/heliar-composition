using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Registration;
using System.Diagnostics.CodeAnalysis;

using Heliar.Composition.Core;

namespace Samples.Data
{
	[ExcludeFromCodeCoverage]
	public class DataDependencyRegistrar : ILibraryDependencyRegistrar
	{
		/// <summary>
		/// Bootstraps the dependencies within this library.
		/// </summary>
		/// <param name="registrations">The dependency registrations/conventions to wire up.</param>
		/// <param name="catalog">An AggregateCatalog that can be added to if dependencies reside in an external assembly, i.e. BCL.</param>
		public void Register(RegistrationBuilder registrations, AggregateCatalog catalog)
		{
			registrations.ForTypesMatching(t => t.Name.EndsWith("Repository"))
				.SetCreationPolicy(CreationPolicy.Shared)
				.ExportInterfaces()
				.Export();
			registrations.ForTypesDerivedFrom<IConnectionFactory>()
				.SetCreationPolicy(CreationPolicy.Shared)
				.ExportInterfaces()
				.Export();
		}
	}
}