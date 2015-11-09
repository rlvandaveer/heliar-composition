using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Registration;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Heliar.Composition.Core;
using Samples.Common;

namespace Samples.ConsoleApp
{
	[ExcludeFromCodeCoverage]
	public class ConsoleDependencyRegistrar : IApplicationDependencyRegistrar
	{
		public void Register(AggregateCatalog catalog)
		{
			var conventions = new RegistrationBuilder();
			conventions.ForTypesDerivedFrom<ILogger>()
				.SetCreationPolicy(CreationPolicy.Shared)
				.ExportInterfaces()
				.Export();
			catalog.Catalogs.Add(new AssemblyCatalog(typeof(SampleLogger).Assembly, conventions));
		}
	}
}