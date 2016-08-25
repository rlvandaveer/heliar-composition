using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Registration;
using System.Diagnostics.CodeAnalysis;

using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Heliar.Composition.Core.Tests
{
	[TestClass]
	[ExcludeFromCodeCoverage]
	public class ContainerBootstrapperTest
	{
		const string assemblyExpression = "Heliar*.dll";

		[TestMethod]
		public void EmptyCtorTest()
		{
			var sut = new ContainerBootstrapper();
			sut.Should().NotBeNull();
			sut.UseAssemblyNamingConvention.Should().Be(false);
		}

		[TestMethod]
		public void ConventionOnlyBootstrapTest()
		{
			var sut = new ContainerBootstrapper(assemblyExpression);
			sut.Should().NotBeNull();
			sut.AssemblyNamingConvention.Should().Be(assemblyExpression);
			sut.UseAssemblyNamingConvention.Should().Be(true);
			var result = sut.Bootstrap();
			result.Should().NotBeNull();
			result.Catalog.Parts.Should().Contain(p => (p as System.ComponentModel.Composition.Primitives.ICompositionElement).DisplayName == "Heliar.Composition.Core.Tests.TestApplicationDependencyRegistrar");
			result.Catalog.Parts.Should().Contain(p => (p as System.ComponentModel.Composition.Primitives.ICompositionElement).DisplayName == "Heliar.Composition.Core.Tests.Foo");
			result.Catalog.Parts.Should().NotContain(p => (p as System.ComponentModel.Composition.Primitives.ICompositionElement).DisplayName == "Samples.Business.BusinessDependencyRegistrar");
			result.Catalog.Parts.Should().NotContain(p => (p as System.ComponentModel.Composition.Primitives.ICompositionElement).DisplayName == "Samples.Data.DataDependencyRegistrar");

		}

		[TestMethod]
		public void CatalogOnlyBootstrapTest()
		{
			var catalog = new AssemblyCatalog(typeof(CatalogBootstrapperTest).Assembly);
			var sut = new ContainerBootstrapper(null, catalog);
			sut.Should().NotBeNull();
			sut.AssemblyNamingConvention.Should().BeNull();
			sut.UseAssemblyNamingConvention.Should().Be(false);
			var result = sut.Bootstrap();
			result.Should().NotBeNull();
			result.Catalog.Parts.Should().Contain(p => (p as System.ComponentModel.Composition.Primitives.ICompositionElement).DisplayName == "Heliar.Composition.Core.Tests.TestApplicationDependencyRegistrar");
			result.Catalog.Parts.Should().Contain(p => (p as System.ComponentModel.Composition.Primitives.ICompositionElement).DisplayName == "Samples.Business.BusinessDependencyRegistrar");
			result.Catalog.Parts.Should().Contain(p => (p as System.ComponentModel.Composition.Primitives.ICompositionElement).DisplayName == "Samples.Data.DataDependencyRegistrar");
			result.Catalog.Parts.Should().Contain(p => (p as System.ComponentModel.Composition.Primitives.ICompositionElement).DisplayName == "Heliar.Composition.Core.Tests.Foo");
			result.Catalog.Parts.Should().Contain(p => (p as System.ComponentModel.Composition.Primitives.ICompositionElement).DisplayName == "Samples.Business.CustomerService");
			result.Catalog.Parts.Should().Contain(p => (p as System.ComponentModel.Composition.Primitives.ICompositionElement).DisplayName == "Samples.Data.CustomerRepository");
		}

		[TestMethod]
		public void CatalogAndConventionBootstrapTest()
		{
			var sut = new ContainerBootstrapper(assemblyExpression, new AssemblyCatalog(typeof(CatalogBootstrapperTest).Assembly));
			sut.Should().NotBeNull();
			sut.AssemblyNamingConvention.Should().Be(assemblyExpression);
			sut.UseAssemblyNamingConvention.Should().Be(true);
			var result = sut.Bootstrap();
			result.Should().NotBeNull();
			result.Catalog.Parts.Should().Contain(p => (p as System.ComponentModel.Composition.Primitives.ICompositionElement).DisplayName == "Heliar.Composition.Core.Tests.TestApplicationDependencyRegistrar");
			result.Catalog.Parts.Should().Contain(p => (p as System.ComponentModel.Composition.Primitives.ICompositionElement).DisplayName == "Heliar.Composition.Core.Tests.Foo");
			result.Catalog.Parts.Should().NotContain(p => (p as System.ComponentModel.Composition.Primitives.ICompositionElement).DisplayName == "Samples.Business.BusinessDependencyRegistrar");
			result.Catalog.Parts.Should().NotContain(p => (p as System.ComponentModel.Composition.Primitives.ICompositionElement).DisplayName == "Samples.Data.DataDependencyRegistrar");
		}

		[TestMethod]
		public void AssemblyBootstrapTest()
		{
			var sut = new ContainerBootstrapper();
			sut.Should().NotBeNull();
			var result = sut.Bootstrap(null, typeof(Samples.Business.BusinessDependencyRegistrar).Assembly, typeof(Samples.Data.DataDependencyRegistrar).Assembly);
			result.Should().NotBeNull();
			result.Catalog.Parts.Should().Contain(p => (p as System.ComponentModel.Composition.Primitives.ICompositionElement).DisplayName == "Heliar.Composition.Core.Tests.TestApplicationDependencyRegistrar");
			result.Catalog.Parts.Should().Contain(p => (p as System.ComponentModel.Composition.Primitives.ICompositionElement).DisplayName == "Samples.Business.BusinessDependencyRegistrar");
			result.Catalog.Parts.Should().Contain(p => (p as System.ComponentModel.Composition.Primitives.ICompositionElement).DisplayName == "Samples.Data.DataDependencyRegistrar");
			result.Catalog.Parts.Should().Contain(p => (p as System.ComponentModel.Composition.Primitives.ICompositionElement).DisplayName == "Heliar.Composition.Core.Tests.Foo");
			result.Catalog.Parts.Should().Contain(p => (p as System.ComponentModel.Composition.Primitives.ICompositionElement).DisplayName == "Samples.Business.CustomerService");
			result.Catalog.Parts.Should().Contain(p => (p as System.ComponentModel.Composition.Primitives.ICompositionElement).DisplayName == "Samples.Data.CustomerRepository");
		}
	}
}