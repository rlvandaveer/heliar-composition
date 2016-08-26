// ***********************************************************************
// Assembly         : Heliar.Composition.Core.Tests
// Author           : R. L. Vandaveer
// Created          : 10-26-2015
//
// Last Modified By : R. L. Vandaveer
// Last Modified On : 08-26-2016
// ***********************************************************************
// <copyright file="AssemblyExtensionsTest.cs" company="">
//     Copyright © R. L. Vandaveer 2015. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;

using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Heliar.Composition.Core.Tests
{
	/// <summary>
	/// Class AssemblyExtensionsTest.
	/// </summary>
	[TestClass]
	[ExcludeFromCodeCoverage]
	public class AssemblyExtensionsTest
	{
		/// <summary>
		/// The directory returned from GetCodeBaseDirectory should match the executing assembly's CodeBase directory.
		/// </summary>
		[TestMethod]
		public void GetCodeBaseDirectoryShouldMatchExecutingAssemblys()
		{
			string codeBase = Assembly.GetExecutingAssembly().CodeBase;
			UriBuilder uri = new UriBuilder(codeBase);
			string path = Uri.UnescapeDataString(uri.Path);
			string testValue = Path.GetDirectoryName(path);
			testValue.Should().BeEquivalentTo(typeof(AssemblyExtensionsTest).Assembly.GetCodeBaseDirectory());
		}
	}
}
