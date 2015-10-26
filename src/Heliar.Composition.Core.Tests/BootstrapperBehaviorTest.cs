using System;
using System.ComponentModel.Composition.Hosting;
using System.Diagnostics.CodeAnalysis;

using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Heliar.Composition.Core.Tests
{
	[TestClass]
	[ExcludeFromCodeCoverage]
	public class BootstrapperBehaviorTest
	{
		[TestMethod]
		public void AddCatalogTest()
		{
			var sut = new TestBootstrapperBehavior(null);
			sut.AddCatalog(new AssemblyCatalog(typeof(BootstrapperBehaviorTest).Assembly));
			sut.Catalog.Catalogs.Should().HaveCount(1);
		}

		[TestMethod]
		public void BootstrapAssmebliesTest()
		{
			var sut = new TestBootstrapperBehavior(null);
			sut.AddCatalog(new AssemblyCatalog(typeof(BootstrapperBehaviorTest).Assembly));
			sut.Catalog.Catalogs.Should().HaveCount(1);
		}
	}
}