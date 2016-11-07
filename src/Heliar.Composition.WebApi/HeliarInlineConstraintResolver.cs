// ***********************************************************************
// Assembly         : Heliar.Composition.WebApi
// Author           : R. L. Vandaveer
// Created          : 10-15-2015
//
// Last Modified By : R. L. Vandaveer
// Last Modified On : 11-13-2015
// ***********************************************************************
// <copyright file="HeliarInlineConstraintResolver.cs" company="">
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
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web.Http.Routing;

using Heliar.Composition.Web;

namespace Heliar.Composition.WebApi
{
	/// <summary>
	/// Resolves constraints and their dependencies for attributed routes. Uses the default list of constraint resolvers that come with
	/// <see cref="DefaultInlineConstraintResolver" /> plus any wired up using MEF.
	/// </summary>
	/// <seealso cref="System.Web.Http.Routing.IInlineConstraintResolver" />
	public class HeliarInlineConstraintResolver : IInlineConstraintResolver
	{
		/// <summary>
		/// The default resolver
		/// </summary>
		private readonly DefaultInlineConstraintResolver defaultResolver = new DefaultInlineConstraintResolver();

		/// <summary>
		/// The constraints
		/// </summary>
		private readonly Dictionary<string, Lazy<IHttpRouteConstraint, IHttpRouteConstraintMetadata>> constraints = new Dictionary<string, Lazy<IHttpRouteConstraint, IHttpRouteConstraintMetadata>>();

		/// <summary>
		/// Initializes a new instance of the <see cref="HeliarInlineConstraintResolver" /> class.
		/// </summary>
		/// <param name="constraints">The constraints.</param>
		public HeliarInlineConstraintResolver(IEnumerable<Lazy<IHttpRouteConstraint, IHttpRouteConstraintMetadata>> constraints)
		{
			foreach (var constraint in constraints)
			{
				//if (!this.constraints.ContainsKey(constraint.Metadata.ConstraintName))
					this.constraints.Add(constraint.Metadata.ConstraintName, constraint);
			}
		}

		/// <summary>
		/// Resolves the constraint and its dependencies.
		/// </summary>
		/// <param name="inlineConstraint">The inline constraint.</param>
		/// <returns>IHttpRouteConstraint.</returns>
		public IHttpRouteConstraint ResolveConstraint(string inlineConstraint)
		{
			return this.defaultResolver.ResolveConstraint(inlineConstraint) ??
				this.constraints.FirstOrDefault(kvp => String.CompareOrdinal(inlineConstraint.ToUpper(), kvp.Key.ToUpper()) == 0).Value?.Value;
		}
	}
}