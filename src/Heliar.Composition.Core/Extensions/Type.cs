// ***********************************************************************
// Assembly         : Heliar.Composition.Core
// Author           : Robb
// Created          : 10-15-2015
//
// Last Modified By : Robb
// Last Modified On : 10-15-2015
// ***********************************************************************
// <copyright file="Type.cs" company="">
//	Copyright ©2015 - 2016 R. L. Vandaveer. Permission is hereby granted,
//	free of charge, to any person obtaining a copy of this software and
//	associated documentation files (the "Software"), to deal in the Software
//	without restriction, including without limitation the rights to use, copy,
//	modify, merge, publish, distribute, sublicense, and/or sell copies of the
//	Software, and to permit persons to whom the Software is furnished to do so,
//	subject to the following conditions: The above copyright notice and this
//	permission notice shall be included in all copies or substantial portions
//	of the Software.
//	THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS
//	OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//	FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//	AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//	LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
//	FROM, OUT OF OR IN
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace System
{
	/// <summary>
	/// Provides extensions to the <see cref="Type" /> class.
	/// </summary>
	public static class TypeExtensions
	{
		/// <summary>
		/// Determines whether the type is in a namespace identified by the specified namespace fragment.
		/// </summary>
		/// <param name="type">The type to check.</param>
		/// <param name="namespaceFragment">The namespace fragment.</param>
		/// <returns><c>true</c> if the <see cref="Type" /> is in the namespace identified by the specified namespace fragment; otherwise, <c>false</c>.</returns>
		public static bool IsInNamespace(this Type type, string namespaceFragment)
		{
			return type.Namespace != null && (type.Namespace.EndsWith("." + namespaceFragment) || type.Namespace.Contains("." + namespaceFragment + "."));
		}
	}
}