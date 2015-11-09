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
		public void Register(AggregateCatalog catalog)
		{
			RegistrationBuilder conventions = new RegistrationBuilder();
			conventions.ForTypesMatching(t => t.Name.EndsWith("Repository"))
				.SetCreationPolicy(CreationPolicy.Shared)
				.ExportInterfaces()
				.Export();
			conventions.ForTypesDerivedFrom<IConnectionFactory>()
				.SetCreationPolicy(CreationPolicy.Shared)
				.ExportInterfaces()
				.Export();
			catalog.Catalogs.Add(new AssemblyCatalog(typeof(Customer).Assembly, conventions));
		}
	}
}