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

		#region Constructor Test Scenarios

		/// <summary>
		/// Using the empty constructor should set UseAssemblyNamingConvention to false.
		/// </summary>
		[TestMethod]
		public void UsingEmptyCtorShouldSetUseAssemblyNamingConventionToFalse()
		{
			var sut = new ContainerBootstrapper();
			sut.Should().NotBeNull();
			sut.UseAssemblyNamingConvention.Should().Be(false);
		}

		/// <summary>
		/// Passing only a convention to the constructor should setUseAssemblyNamingConvention to false.
		/// </summary>
		[TestMethod]
		public void PassingOnlyAConventionToCtorShouldSetUseAssemblyNamingConventionToTrue()
		{
			var sut = new ContainerBootstrapper(assemblyExpression);
			sut.Should().NotBeNull();
			sut.AssemblyNamingConvention.Should().Be(assemblyExpression);
			sut.UseAssemblyNamingConvention.Should().Be(true);
		}

		/// <summary>
		/// Passing only a catalog to the constructor should set use assembly naming convention to false.
		/// </summary>
		[TestMethod]
		public void PassingOnlyCatalogToCtorShouldSetUseAssemblyNamingConventionToFalse()
		{
			var catalog = new AssemblyCatalog(typeof(CatalogBootstrapperTest).Assembly);
			var sut = new ContainerBootstrapper(null, catalog);
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
			var sut = new ContainerBootstrapper(assemblyExpression);
			sut.Should().NotBeNull();
			sut.AssemblyNamingConvention.Should().Be(assemblyExpression);
		}

		#endregion

		/// <summary>
		/// Bootstrapping using only an assembly convention should only include test assembly types.
		/// </summary>
		[TestMethod]
		public void BootstrappingUsingOnlyConventionShouldOnlyIncludeTestAssemblyTypes()
		{
			var sut = new ContainerBootstrapper(assemblyExpression);
			sut.Should().NotBeNull();
			var result = sut.Bootstrap();
			result.Should().NotBeNull();
			result.Catalog.Parts.Should().HaveCount(c => c > 0);
			// Registrar types
			result.Catalog.Parts.Should().Contain(p => (p as System.ComponentModel.Composition.Primitives.ICompositionElement).DisplayName == "Heliar.Composition.Core.Tests.TestApplicationDependencyRegistrar");
			result.Catalog.Parts.Should().NotContain(p => (p as System.ComponentModel.Composition.Primitives.ICompositionElement).DisplayName == "Samples.Business.BusinessDependencyRegistrar");
			result.Catalog.Parts.Should().NotContain(p => (p as System.ComponentModel.Composition.Primitives.ICompositionElement).DisplayName == "Samples.Data.DataDependencyRegistrar");
			// Test POCO type
			result.Catalog.Parts.Should().Contain(p => (p as System.ComponentModel.Composition.Primitives.ICompositionElement).DisplayName == "Heliar.Composition.Core.Tests.Foo");
		}

		/// <summary>
		/// Bootstrapping dependencies using an assembly catalog should include test and sample types.
		/// </summary>
		[TestMethod]
		public void BootstrappingUsingOnlyCatalogShouldIncludeTestAndSampleTypes()
		{
			var catalog = new AssemblyCatalog(typeof(CatalogBootstrapperTest).Assembly);
			var sut = new ContainerBootstrapper(null, catalog);
			sut.Should().NotBeNull();
			var result = sut.Bootstrap();
			result.Should().NotBeNull();
			result.Catalog.Should().NotBeNull();
			// Registrars
			result.Catalog.Parts.Should().Contain(p => (p as System.ComponentModel.Composition.Primitives.ICompositionElement).DisplayName == "Heliar.Composition.Core.Tests.TestApplicationDependencyRegistrar");
			result.Catalog.Parts.Should().Contain(p => (p as System.ComponentModel.Composition.Primitives.ICompositionElement).DisplayName == "Samples.Business.BusinessDependencyRegistrar");
			result.Catalog.Parts.Should().Contain(p => (p as System.ComponentModel.Composition.Primitives.ICompositionElement).DisplayName == "Samples.Data.DataDependencyRegistrar");
			// Test/Sample types
			result.Catalog.Parts.Should().Contain(p => (p as System.ComponentModel.Composition.Primitives.ICompositionElement).DisplayName == "Heliar.Composition.Core.Tests.Foo");
			result.Catalog.Parts.Should().Contain(p => (p as System.ComponentModel.Composition.Primitives.ICompositionElement).DisplayName == "Samples.Business.CustomerService");
			result.Catalog.Parts.Should().Contain(p => (p as System.ComponentModel.Composition.Primitives.ICompositionElement).DisplayName == "Samples.Data.CustomerRepository");
			result.Catalog.Parts.Should().Contain(p => (p as System.ComponentModel.Composition.Primitives.ICompositionElement).DisplayName == "Samples.Data.SampleConnectionFactory");
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
			var catalog = new AssemblyCatalog(typeof(ContainerBootstrapperTest).Assembly, conventions);
			var sut = new ContainerBootstrapper("Samples*.dll", catalog);
			sut.Should().NotBeNull();
			var result = sut.Bootstrap();
			result.Should().NotBeNull();
			result.Catalog.Should().NotBeNull();
			// Registrars
			result.Catalog.Parts.Should().Contain(p => (p as System.ComponentModel.Composition.Primitives.ICompositionElement).DisplayName == "Heliar.Composition.Core.Tests.TestApplicationDependencyRegistrar");
			result.Catalog.Parts.Should().Contain(p => (p as System.ComponentModel.Composition.Primitives.ICompositionElement).DisplayName == "Samples.Business.BusinessDependencyRegistrar");
			result.Catalog.Parts.Should().Contain(p => (p as System.ComponentModel.Composition.Primitives.ICompositionElement).DisplayName == "Samples.Data.DataDependencyRegistrar");
			// Test/Sample types
			result.Catalog.Parts.Should().Contain(p => (p as System.ComponentModel.Composition.Primitives.ICompositionElement).DisplayName == "Heliar.Composition.Core.Tests.Foo");
			result.Catalog.Parts.Should().Contain(p => (p as System.ComponentModel.Composition.Primitives.ICompositionElement).DisplayName == "Samples.Business.CustomerService");
			result.Catalog.Parts.Should().Contain(p => (p as System.ComponentModel.Composition.Primitives.ICompositionElement).DisplayName == "Samples.Data.CustomerRepository");
			result.Catalog.Parts.Should().Contain(p => (p as System.ComponentModel.Composition.Primitives.ICompositionElement).DisplayName == "Samples.Data.SampleConnectionFactory");
		}

		/// <summary>
		/// Bootstrapping without an application dependency registrar should throw an exception.
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(ApplicationDependencyRegistrarImplementationException))]
		public void BootstrappingWithoutApplicationDependencyRegistrarShouldThrowException()
		{
			var sut = new ContainerBootstrapper("Foo*.dll");
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
		//var sut = new ContainerBootstrapper("Foo*.dll");
		//sut.Should().NotBeNull();
		//var result = sut.Bootstrap();
		//}

		/// <summary>
		/// Bootstrapping using assembly parameter list should contain sample and test types.
		/// </summary>
		[TestMethod]
		public void BootstrappingUsingAssemblyParamListShouldContainTypesInParamList()
		{
			var sut = new ContainerBootstrapper();
			sut.Should().NotBeNull();
			var result = sut.Bootstrap(null, typeof(Samples.Business.BusinessDependencyRegistrar).Assembly, typeof(Samples.Data.DataDependencyRegistrar).Assembly);
			result.Should().NotBeNull();
			result.Catalog.Should().NotBeNull();
			result.Catalog.Parts.Should().Contain(p => (p as System.ComponentModel.Composition.Primitives.ICompositionElement).DisplayName == "Heliar.Composition.Core.Tests.TestApplicationDependencyRegistrar");
			result.Catalog.Parts.Should().Contain(p => (p as System.ComponentModel.Composition.Primitives.ICompositionElement).DisplayName == "Samples.Business.BusinessDependencyRegistrar");
			result.Catalog.Parts.Should().Contain(p => (p as System.ComponentModel.Composition.Primitives.ICompositionElement).DisplayName == "Samples.Data.DataDependencyRegistrar");
			result.Catalog.Parts.Should().Contain(p => (p as System.ComponentModel.Composition.Primitives.ICompositionElement).DisplayName == "Heliar.Composition.Core.Tests.Foo");
			result.Catalog.Parts.Should().Contain(p => (p as System.ComponentModel.Composition.Primitives.ICompositionElement).DisplayName == "Samples.Business.CustomerService");
			result.Catalog.Parts.Should().Contain(p => (p as System.ComponentModel.Composition.Primitives.ICompositionElement).DisplayName == "Samples.Data.CustomerRepository");
		}
	}
}