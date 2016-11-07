using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
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
		public void Register(RegistrationBuilder registrations)
		{
			registrations.ForTypesDerivedFrom<ILogger>()
				.SetCreationPolicy(CreationPolicy.Shared)
				.ExportInterfaces()
				.Export();
		}
	}
}