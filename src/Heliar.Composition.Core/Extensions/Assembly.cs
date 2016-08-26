// ***********************************************************************
// Assembly         : Heliar.Composition.Core
// Author           : R. L. Vandaveer
// Created          : 10-15-2015
//
// Last Modified By : R. L. Vandaveer
// Last Modified On : 08-26-2016
// ***********************************************************************
// <copyright file="Assembly.cs" company="">
//     Copyright (c) 2016 R.L. Vandaveer. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.IO;

namespace System.Reflection
{
	/// <summary>
	/// Provides extensions to the <see cref="Assembly" /> class.
	/// </summary>
	public static class AssemblyExtensions
	{
		/// <summary>
		/// Gets the assembly's location, i.e. containing directory.
		/// </summary>
		/// <param name="assembly">The assembly whose location to return.</param>
		/// <returns><see cref="System.String" /> representing the assembly location.</returns>
		public static string GetCodeBaseDirectory(this Assembly assembly)
		{
			string codeBase = Assembly.GetExecutingAssembly().CodeBase;
			UriBuilder uri = new UriBuilder(codeBase);
			string path = Uri.UnescapeDataString(uri.Path);
			return Path.GetDirectoryName(path);
		}
	}
}