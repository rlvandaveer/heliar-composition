// ***********************************************************************
// Assembly         : Heliar.Composition.Core.Tests
// Author           : R. L. Vandaveer
// Created          : 10-26-2015
//
// Last Modified By : R. L. Vandaveer
// Last Modified On : 08-26-2016
// ***********************************************************************
// <copyright file="BootstrapperBehaviorTest.cs" company="">
//     Copyright © 2013 - 2016 R. L. Vandaveer
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.ComponentModel.Composition.Hosting;
using System.Diagnostics.CodeAnalysis;

using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Heliar.Composition.Core.Tests
{
	/// <summary>
	/// Facilitates testing abstract BootstrapperBehavior class.
	/// </summary>
	[TestClass]
	[ExcludeFromCodeCoverage]
	public class BootstrapperBehaviorTest
	{
		/// <summary>
		/// Initialized bootstrapper should have empty catalog.
		/// </summary>
		[TestMethod]
		public void InitializedBootstrapperShouldHaveEmptyCatalog()
		{
			var sut = new TestBootstrapperBehavior(null);
			sut.Should().NotBeNull();
			sut.Catalog.Should().NotBeNull();
			sut.Catalog.Catalogs.Should().HaveCount(0);
		}

		/// <summary>
		/// A catalog that has been added to a Bootstrapper should be present in the Bootstrapper.
		/// </summary>
		[TestMethod]
		public void BootstrapperShouldContainAddedCatalog()
		{
			var catalog = new AssemblyCatalog(typeof(BootstrapperBehaviorTest).Assembly);
			var sut = new TestBootstrapperBehavior(null);
			sut.Should().NotBeNull();
			sut.Catalog.Should().NotBeNull();
			sut.AddCatalog(catalog);
			sut.Catalog.Catalogs.Should().HaveCount(1);
			sut.Catalog.Catalogs.Should().Contain(catalog);
		}
	}
}