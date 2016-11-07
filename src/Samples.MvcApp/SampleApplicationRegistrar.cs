using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Registration;
using System.Linq;
using System.Reflection.Context;
using System.Web;

using Heliar.Composition.Core;
using Samples.Common;

namespace Samples.MvcApp
{
	public class SampleApplicationRegistrar : IApplicationDependencyRegistrar
	{
		/// <summary>
		/// Registers the dependencies (via convention) within this application.
		/// </summary>
		/// <param name="registrations">The dependency registrations/conventions to wire up.</param>
		/// <param name="catalog">An AggregateCatalog that can be added to if dependencies reside in an external assembly, i.e. BCL.</param>
		public void Register(RegistrationBuilder registrations, AggregateCatalog catalog)
		{
			registrations.ForTypesDerivedFrom<ILogger>()
				.SetCreationPolicy(CreationPolicy.Shared)
				.ExportInterfaces()
				.Export();
		}
	}
}