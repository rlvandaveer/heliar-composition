// ***********************************************************************
// Assembly         : Heliar.Composition.Core.Tests
// Author           : R. L. Vandaveer
// Created          : 10-23-2015
//
// Last Modified By : R. L. Vandaveer
// Last Modified On : 08-26-2016
// ***********************************************************************
// <copyright file="CatalogBootstrapperTest.cs" company="">
//     Copyright © 2013 - 2016 R. L. Vandaveer
// </copyright>
// <summary></summary>
// ***********************************************************************
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
	/// <summary>
	/// Facilitates testing CatalogBootstrapper class.
	/// </summary>
	[TestClass]
	[ExcludeFromCodeCoverage]
	public class CatalogBootstrapperTest
	{
		/// <summary>
		/// An expression for adding assemblies to a catalog
		/// </summary>
		const string assemblyExpression = "Heliar*.dll";

		#region Constructor Test Scenarios

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

		#endregion

		#region Boostrap Test Scenarios

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
		/// Bootstrapping using assembly parameter list should contain sample and test types.
		/// </summary>
		[TestMethod]
		public void BootstrappingUsingAssemblyParamListShouldContainTypesInParamList()
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

		//TODO: RLV - Write registrar finisher test(s)

		#endregion
	}
}