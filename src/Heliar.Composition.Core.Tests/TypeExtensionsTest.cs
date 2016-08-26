// ***********************************************************************
// Assembly         : Heliar.Composition.Core.Tests
// Author           : R. L. Vandaveer
// Created          : 10-26-2015
//
// Last Modified By : R. L. Vandaveer
// Last Modified On : 08-26-2016
// ***********************************************************************
// <copyright file="TypeExtensionsTest.cs" company="">
//     Copyright © 2015 R. L. Vandaveer 
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Diagnostics.CodeAnalysis;

using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Heliar.Composition.Core.Tests
{
	/// <summary>
	/// Tests for TypeExtensions class.
	/// </summary>
	[TestClass]
	[ExcludeFromCodeCoverage]
	public class TypeExtensionsTest
	{
		/// <summary>
		/// Tests for whether a fragment at the end of a namespace is in that namespace should return true.
		/// </summary>
		[TestMethod]
		public void IsInNamespaceShouldBeTrueForFragmentsAtTheEndOfANamespace()
		{
			typeof(BootstrapperBehavior).IsInNamespace("Core").Should().Be(true);
		}

		/// <summary>
		/// Tests for whether a fragment at the end of a namespace is in that namespace should return true.
		/// </summary>
		[TestMethod]
		public void IsInNamespaceShouldBeTrueForFragmentsAtTheEndOfANamespace2()
		{
			typeof(TypeExtensionsTest).IsInNamespace("Tests").Should().Be(true);
		}

		/// <summary>
		/// Tests for whether a fragment at the beginning of a namespace is in that namespace should return false.
		/// </summary>
		[TestMethod]
		public void IsInNamespaceShouldBeFalseForFragmentsAtTheStartOfANamespace()
		{
			typeof(TypeExtensionsTest).IsInNamespace("Heliar").Should().Be(false);
		}

		/// <summary>
		/// Tests for whether a fragment at the middle of a namespace is in that namespace should return true.
		/// </summary>
		[TestMethod]
		public void IsInNamespaceShouldBeTrueForFragmentsContainedInNamespace()
		{
			typeof(TypeExtensionsTest).IsInNamespace("Core").Should().Be(true);
		}

	}
}