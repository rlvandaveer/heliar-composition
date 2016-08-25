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
		public void Register(RegistrationBuilder registrations)
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