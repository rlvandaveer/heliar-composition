using System;
using System.Diagnostics.CodeAnalysis;

using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Heliar.Composition.Core.Tests
{
	[TestClass]
	[ExcludeFromCodeCoverage]
	public class TypeExtensionsTest
	{
		[TestMethod]
		public void EndsWithFragmentTest()
		{
			typeof(TypeExtensionsTest).IsInNamespace("Tests").Should().Be(true);
		}

		[TestMethod]
		public void StartsWithFragmentTest()
		{
			typeof(TypeExtensionsTest).IsInNamespace("Heliar").Should().Be(false);
		}

		[TestMethod]
		public void ContainsFragmentTest()
		{
			typeof(TypeExtensionsTest).IsInNamespace("Core").Should().Be(true);
		}


		[TestMethod]
		public void EndsWithFragmentTest2()
		{
			typeof(BootstrapperBehavior).IsInNamespace("Core").Should().Be(true);
		}
	}
}