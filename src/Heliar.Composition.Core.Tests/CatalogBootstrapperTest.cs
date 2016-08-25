using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Registration;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;

namespace Heliar.Composition.Core.Tests
{
	[TestClass]
	[ExcludeFromCodeCoverage]
	public class CatalogBootstrapperTest
	{
		const string assemblyExpression = "Heliar*.dll";

		/// <summary>
		/// Using the empty constructor should set use assembly naming convention to false.
		/// </summary>
		[TestMethod]
		public void UsingEmptyCtorShouldSetUseAssemblyNamingConventionToFalse()
		{
			var sut = new CatalogBootstrapper();
			sut.Should().NotBeNull();
			sut.UseAssemblyNamingConvention.Should().Be(false);
		}

		/// <summary>
		/// Passing only an assembly convention to the constructor should set use assembly naming convention to true.
		/// </summary>
		[TestMethod]
		public void PassingOnlyAConventionToCtorShouldSetUseAssemblyNamingConventionToTrue()
		{
			var sut = new CatalogBootstrapper(assemblyExpression);
			sut.Should().NotBeNull();
			sut.UseAssemblyNamingConvention.Should().Be(true);
		}

		/// <summary>
		/// Passing only a catalog to the constructor should set use assembly naming convention to false.
		/// </summary>
		[TestMethod]
		public void PassingOnlyCatalogToCtorShouldSetUseAssemblyNamingConventionToFalse()
		{
			var catalog = new AssemblyCatalog(typeof(CatalogBootstrapperTest).Assembly);
			var sut = new CatalogBootstrapper(null, catalog);
			sut.Should().NotBeNull();
			sut.AssemblyNamingConvention.Should().BeNull();
			sut.UseAssemblyNamingConvention.Should().Be(false);
		}

		/// <summary>
		/// Expression passed to convention constructor should match the assembly naming convention.
		/// </summary>
		[TestMethod]
		public void ExpressionPassedToConventionCtorShouldMatchAssemblyNamingConvention()
		{
			var sut = new CatalogBootstrapper(assemblyExpression);
			sut.Should().NotBeNull();
			sut.AssemblyNamingConvention.Should().Be(assemblyExpression);
		}

		/// <summary>
		/// Bootstrapping using only an assembly convention should only include test assembly types.
		/// </summary>
		[TestMethod]
		public void BootstrappingUsingOnlyConventionShouldOnlyIncludeTestAssemblyTypes()
		{
			var sut = new CatalogBootstrapper(assemblyExpression);
			sut.Should().NotBeNull();
			var result = sut.Bootstrap();
			result.Should().NotBeNull();
			result.Catalogs.Should().HaveCount(c => c > 0);
			// Registrar types
			result.Catalogs.SelectMany(c => c.Parts).Should().Contain(p => (p as System.ComponentModel.Composition.Primitives.ICompositionElement).DisplayName == "Heliar.Composition.Core.Tests.TestApplicationDependencyRegistrar");
			result.Catalogs.SelectMany(c => c.Parts).Should().NotContain(p => (p as System.ComponentModel.Composition.Primitives.ICompositionElement).DisplayName == "Samples.Business.BusinessDependencyRegistrar");
			result.Catalogs.SelectMany(c => c.Parts).Should().NotContain(p => (p as System.ComponentModel.Composition.Primitives.ICompositionElement).DisplayName == "Samples.Data.DataDependencyRegistrar");
			// Test POCO type
			result.Catalogs.SelectMany(c => c.Parts).Should().Contain(p => (p as System.ComponentModel.Composition.Primitives.ICompositionElement).DisplayName == "Heliar.Composition.Core.Tests.Foo");
		}

		/// <summary>
		/// Bootstrapping dependencies using an assembly catalog should include test and sample types.
		/// </summary>
		[TestMethod]
		public void BootstrappingUsingOnlyCatalogShouldIncludeTestAndSampleTypes()
		{
			var catalog = new AssemblyCatalog(typeof(CatalogBootstrapperTest).Assembly);
			var sut = new CatalogBootstrapper(null, catalog);
			sut.Should().NotBeNull();
			var result = sut.Bootstrap();
			result.Should().NotBeNull();
			result.Catalogs.Should().HaveCount(c => c > 0);
			// Registrars
			result.Catalogs.SelectMany(c => c.Parts).Should().Contain(p => (p as System.ComponentModel.Composition.Primitives.ICompositionElement).DisplayName == "Heliar.Composition.Core.Tests.TestApplicationDependencyRegistrar");
			result.Catalogs.SelectMany(c => c.Parts).Should().Contain(p => (p as System.ComponentModel.Composition.Primitives.ICompositionElement).DisplayName == "Samples.Business.BusinessDependencyRegistrar");
			result.Catalogs.SelectMany(c => c.Parts).Should().Contain(p => (p as System.ComponentModel.Composition.Primitives.ICompositionElement).DisplayName == "Samples.Data.DataDependencyRegistrar");
			// Test/Sample types
			result.Catalogs.SelectMany(c => c.Parts).Should().Contain(p => (p as System.ComponentModel.Composition.Primitives.ICompositionElement).DisplayName == "Heliar.Composition.Core.Tests.Foo");
			result.Catalogs.SelectMany(c => c.Parts).Should().Contain(p => (p as System.ComponentModel.Composition.Primitives.ICompositionElement).DisplayName == "Samples.Business.CustomerService");
			result.Catalogs.SelectMany(c => c.Parts).Should().Contain(p => (p as System.ComponentModel.Composition.Primitives.ICompositionElement).DisplayName == "Samples.Data.CustomerRepository");
		}

		/// <summary>
		/// Bootstrapping using a catalog and convention should include test and sample parts.
		/// </summary>
		[TestMethod]
		public void BootstrappingUsingCatalogAndConventionShouldIncludeTestAndSampleParts()
		{
			// Build pre-wired assembly catalog
			RegistrationBuilder conventions = new RegistrationBuilder();
			conventions.ForTypesDerivedFrom<IApplicationDependencyRegistrar>()
				.SetCreationPolicy(CreationPolicy.Shared)
				.ExportInterfaces()
				.Export();
			var catalog = new AssemblyCatalog(typeof(CatalogBootstrapperTest).Assembly, conventions);
			var sut = new CatalogBootstrapper("Samples*.dll", catalog);
			sut.Should().NotBeNull();
			var result = sut.Bootstrap();
			result.Should().NotBeNull();
			// Registrars
			result.Catalogs.SelectMany(c => c.Parts).Should().Contain(p => (p as System.ComponentModel.Composition.Primitives.ICompositionElement).DisplayName == "Heliar.Composition.Core.Tests.TestApplicationDependencyRegistrar");
			result.Catalogs.SelectMany(c => c.Parts).Should().Contain(p => (p as System.ComponentModel.Composition.Primitives.ICompositionElement).DisplayName == "Samples.Business.BusinessDependencyRegistrar");
			result.Catalogs.SelectMany(c => c.Parts).Should().Contain(p => (p as System.ComponentModel.Composition.Primitives.ICompositionElement).DisplayName == "Samples.Data.DataDependencyRegistrar");
			// Test/Sample types
			result.Catalogs.SelectMany(c => c.Parts).Should().Contain(p => (p as System.ComponentModel.Composition.Primitives.ICompositionElement).DisplayName == "Heliar.Composition.Core.Tests.Foo");
			result.Catalogs.SelectMany(c => c.Parts).Should().Contain(p => (p as System.ComponentModel.Composition.Primitives.ICompositionElement).DisplayName == "Samples.Business.CustomerService");
			result.Catalogs.SelectMany(c => c.Parts).Should().Contain(p => (p as System.ComponentModel.Composition.Primitives.ICompositionElement).DisplayName == "Samples.Data.CustomerRepository");
			result.Catalogs.SelectMany(c => c.Parts).Should().Contain(p => (p as System.ComponentModel.Composition.Primitives.ICompositionElement).DisplayName == "Samples.Data.SampleConnectionFactory");
		}
		/// <summary>
		/// Bootstrapping without an application dependency registrar should throw an exception.
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(ApplicationDependencyRegistrarImplementationException))]
		public void BootstrappingWithoutApplicationDependencyRegistrarShouldThrowException()
		{
			var sut = new CatalogBootstrapper("Foo*.dll");
			sut.Should().NotBeNull();
			var result = sut.Bootstrap();
		}

		/// <summary>
		/// Bootstrapping with more than one application dependency registrar should throw exception.
		/// </summary>
		//[TestMethod]
		//[ExpectedException(typeof(ApplicationDependencyRegistrarImplementationException))]
		//public void BootstrappingWithMoreThanOneApplicationDependencyRegistrarShouldThrowException()
		//{
			//TODO: RLV - Determine how to test two IApplicationDependencyRegistrars
			//var sut = new CatalogBootstrapper("Foo*.dll");
			//sut.Should().NotBeNull();
			//var result = sut.Bootstrap();
		//}

		/// <summary>
		/// Test should be able to compose <see cref="Heliar.Composition.Core.Tests.Foo"/>.
		/// </summary>
		[TestMethod]
		public void ShouldBeAbleToComposeFooType()
		{
			var sut = new CatalogBootstrapper(assemblyExpression);
			sut.Should().NotBeNull();
			sut.AssemblyNamingConvention.Should().Be(assemblyExpression);
			sut.UseAssemblyNamingConvention.Should().Be(true);
			var catalog = sut.Bootstrap();
			catalog.Should().NotBeNull();
			catalog.Catalogs.Should().HaveCount(2);
			var container = new CompositionContainer(catalog);
			var result = container.GetExportedValue<IFoo>();
			result.Should().NotBeNull();
			result.Name.Should().Be("Foo");
		}

		/// <summary>
		/// Bootstrapping using assembly parameter list should contain sample and test types.
		/// </summary>
		[TestMethod]
		public void BootstrappingUsingAssemblyParamListShouldContainSampleAndTestTypes()
		{
			var sut = new CatalogBootstrapper();
			sut.Should().NotBeNull();
			sut.AssemblyNamingConvention.Should().BeNull();
			sut.UseAssemblyNamingConvention.Should().Be(false);
			var result = sut.Bootstrap(null, typeof(Samples.Business.BusinessDependencyRegistrar).Assembly, typeof(Samples.Data.DataDependencyRegistrar).Assembly);
			result.Should().NotBeNull();
			result.Catalogs.Should().HaveCount(c => c > 0);
			result.Catalogs.SelectMany(c => c.Parts).Should().Contain(p => (p as System.ComponentModel.Composition.Primitives.ICompositionElement).DisplayName == "Heliar.Composition.Core.Tests.TestApplicationDependencyRegistrar");
			result.Catalogs.SelectMany(c => c.Parts).Should().Contain(p => (p as System.ComponentModel.Composition.Primitives.ICompositionElement).DisplayName == "Samples.Business.BusinessDependencyRegistrar");
			result.Catalogs.SelectMany(c => c.Parts).Should().Contain(p => (p as System.ComponentModel.Composition.Primitives.ICompositionElement).DisplayName == "Samples.Data.DataDependencyRegistrar");
			result.Catalogs.SelectMany(c => c.Parts).Should().Contain(p => (p as System.ComponentModel.Composition.Primitives.ICompositionElement).DisplayName == "Heliar.Composition.Core.Tests.Foo");
			result.Catalogs.SelectMany(c => c.Parts).Should().Contain(p => (p as System.ComponentModel.Composition.Primitives.ICompositionElement).DisplayName == "Samples.Business.CustomerService");
			result.Catalogs.SelectMany(c => c.Parts).Should().Contain(p => (p as System.ComponentModel.Composition.Primitives.ICompositionElement).DisplayName == "Samples.Data.CustomerRepository");
		}
	}
}