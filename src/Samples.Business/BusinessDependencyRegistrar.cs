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

namespace Samples.Business
{
	[ExcludeFromCodeCoverage]
	public class BusinessDependencyRegistrar : ILibraryDependencyRegistrar
	{
		public void Register(RegistrationBuilder registrations)
		{
			registrations.ForTypesMatching(t => t.Name.EndsWith("Service"))
				.SetCreationPolicy(CreationPolicy.Shared)
				.ExportInterfaces()
				.Export();
		}
	}
}