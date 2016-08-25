using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Registration;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heliar.Composition.Core.Tests
{
	[ExcludeFromCodeCoverage]
	class TestApplicationDependencyRegistrar : IApplicationDependencyRegistrar
	{
		public void Register(RegistrationBuilder registrations)
		{
			registrations.ForType<Foo>()
				.SetCreationPolicy(CreationPolicy.NonShared)
				.ExportInterfaces()
				.Export();
		}
	}

	interface IFoo
	{
		string Name { get; set; }
	}

	[ExcludeFromCodeCoverage]
	class Foo : IFoo
	{
		public string Name { get; set; }
		public Foo() { this.Name = "Foo"; }
	}

	[ExcludeFromCodeCoverage]
	class FooBar : IFoo
	{
		public string Name { get; set; }
		public FooBar() { this.Name = "Foo Bar"; }
	}
}