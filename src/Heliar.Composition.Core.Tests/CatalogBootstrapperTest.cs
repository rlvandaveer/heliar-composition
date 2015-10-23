using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Registration;

using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Heliar.Composition.Core.Tests
{
	[TestClass]
	public class CatalogBootstrapperTest
	{
		[TestMethod]
		public void EmptyCtorTest()
		{
			var sut = new CatalogBootstrapper();
			sut.Should().NotBeNull();
			sut.UseAssemblyNamingConvention.Should().Be(false);
		}

		[TestMethod]
		public void ConventionOnlyBootstrapTest()
		{
			var sut = new CatalogBootstrapper("Heliar*.dll");
			sut.Should().NotBeNull();
			sut.AssemblyNamingConvention.Should().Be("Heliar*.dll");
			sut.UseAssemblyNamingConvention.Should().Be(true);
			var result = sut.Bootstrap();
			result.Should().NotBeNull();
			result.Catalogs.Should().HaveCount(2);
		}

		[TestMethod]
		public void CatalogOnlyBootstrapTest()
		{
			var conventions = new RegistrationBuilder();
			conventions.ForTypesDerivedFrom<IApplicationDependencyRegistrar>()
				.SetCreationPolicy(CreationPolicy.Shared)
				.ExportInterfaces()
				.Export();
			var catalog = new AssemblyCatalog(typeof(CatalogBootstrapperTest).Assembly, conventions);
			var sut = new CatalogBootstrapper(null, catalog);
			sut.Should().NotBeNull();
			sut.AssemblyNamingConvention.Should().BeNull();
			sut.UseAssemblyNamingConvention.Should().Be(false);
			var result = sut.Bootstrap();
			result.Should().NotBeNull();
			result.Catalogs.Should().HaveCount(2);
		}

		[TestMethod]
		public void CatalogAndConventionBootstrapTest()
		{
			var sut = new CatalogBootstrapper("Heliar*.dll", new AssemblyCatalog(typeof(CatalogBootstrapperTest).Assembly));
			sut.Should().NotBeNull();
			sut.AssemblyNamingConvention.Should().Be("Heliar*.dll");
			sut.UseAssemblyNamingConvention.Should().Be(true);
			var result = sut.Bootstrap();
			result.Should().NotBeNull();
			result.Catalogs.Should().HaveCount(3);
		}

		[TestMethod]
		[ExpectedException(typeof(ApplicationDependencyRegistrarImplementationException))]
		public void NoApplicationDependencyRegistrarTest()
		{
			var sut = new CatalogBootstrapper();
			sut.Should().NotBeNull();
			sut.UseAssemblyNamingConvention.Should().Be(false);
			var result = sut.Bootstrap();
		}

		[TestMethod]
		public void CompositionBootstrapTest()
		{
			var sut = new CatalogBootstrapper("Heliar*.dll");
			sut.Should().NotBeNull();
			sut.AssemblyNamingConvention.Should().Be("Heliar*.dll");
			sut.UseAssemblyNamingConvention.Should().Be(true);
			var catalog = sut.Bootstrap();
			catalog.Should().NotBeNull();
			catalog.Catalogs.Should().HaveCount(2);
			var container = new CompositionContainer(catalog);
			var result = container.GetExportedValue<IFoo>();
			result.Should().NotBeNull();
		}
	}
}