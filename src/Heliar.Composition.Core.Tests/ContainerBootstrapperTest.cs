﻿using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Registration;

using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Heliar.Composition.Core.Tests
{
	[TestClass]
	public class ContainerBootstrapperTest
	{
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
			var sut = new ContainerBootstrapper("Heliar*.dll");
			sut.Should().NotBeNull();
			sut.AssemblyNamingConvention.Should().Be("Heliar*.dll");
			sut.UseAssemblyNamingConvention.Should().Be(true);
			var result = sut.Bootstrap();
			result.Should().NotBeNull();
			result.Catalog.Parts.Should().HaveCount(2);

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
			var sut = new ContainerBootstrapper(null, catalog);
			sut.Should().NotBeNull();
			sut.AssemblyNamingConvention.Should().BeNull();
			sut.UseAssemblyNamingConvention.Should().Be(false);
			var result = sut.Bootstrap();
			result.Should().NotBeNull();
			result.Catalog.Parts.Should().HaveCount(2);
		}

		[TestMethod]
		public void CatalogAndConventionBootstrapTest()
		{
			var sut = new ContainerBootstrapper("Heliar*.dll", new AssemblyCatalog(typeof(CatalogBootstrapperTest).Assembly));
			sut.Should().NotBeNull();
			sut.AssemblyNamingConvention.Should().Be("Heliar*.dll");
			sut.UseAssemblyNamingConvention.Should().Be(true);
			var result = sut.Bootstrap();
			result.Should().NotBeNull();
			result.Catalog.Parts.Should().HaveCount(2);
		}
	}
}