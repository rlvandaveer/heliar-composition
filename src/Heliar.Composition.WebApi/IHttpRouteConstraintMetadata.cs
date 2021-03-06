﻿// ***********************************************************************
// Assembly         : Heliar.Composition.WebApi
// Author           : R. L. Vandaveer
// Created          : 11-11-2015
//
// Last Modified By : R. L. Vandaveer
// Last Modified On : 11-11-2015
// ***********************************************************************
// <copyright file="IHttpRouteConstraintMetadata.cs" company="">
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
//	FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS
//	IN THE SOFTWARE.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace Heliar.Composition.WebApi
{
	/// <summary>
	/// Represents metadata for <see cref="IHttpRouteConstraint" />s. Describes implementing types so that they can be wired up by MEF and retrieved
	/// by <see cref="HeliarInlineConstraintResolver" />.
	/// </summary>
	public interface IHttpRouteConstraintMetadata
	{
		/// <summary>
		/// Gets the canonical name of the constraint.
		/// </summary>
		/// <value>The name of the constraint.</value>
		string ConstraintName { get; }
	}
}