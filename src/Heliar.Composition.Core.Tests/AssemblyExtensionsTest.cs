using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;

using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Heliar.Composition.Core.Tests
{
	[TestClass]
	[ExcludeFromCodeCoverage]
	public class AssemblyExtensionsTest
	{
		[TestMethod]
		public void GetCodeBaseDirectoryTest()
		{
			string codeBase = Assembly.GetExecutingAssembly().CodeBase;
			UriBuilder uri = new UriBuilder(codeBase);
			string path = Uri.UnescapeDataString(uri.Path);
			string testValue = Path.GetDirectoryName(path);
			testValue.Should().BeEquivalentTo(typeof(AssemblyExtensionsTest).Assembly.GetCodeBaseDirectory());
		}
	}
}
