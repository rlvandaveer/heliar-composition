// ***********************************************************************
// Assembly         : Heliar.Composition.WebApi
// Author           : R. L. Vandaveer
// Created          : 10-15-2015
//
// Last Modified By : R. L. Vandaveer
// Last Modified On : 10-15-2015
// ***********************************************************************
// <copyright file="CompositionScopedDependencyResolver.cs" company="">
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
using System.Web.Http.Dependencies;

using Heliar.Composition.Web;

namespace Heliar.Composition.WebApi
{
	/// <summary>
	/// Resolves shared or web request scoped dependencies using a composition provider.
	/// </summary>
	/// <seealso cref="Heliar.Composition.WebApi.CompositionDependencyScope" />
	/// <seealso cref="System.Web.Http.Dependencies.IDependencyResolver" />
	public class CompositionScopedDependencyResolver : CompositionDependencyScope, IDependencyResolver
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="CompositionScopedDependencyResolver" /> class.
		/// </summary>
		public CompositionScopedDependencyResolver() : base(HeliarCompositionProvider.ApplicationScopedContainer) { }

		/// <summary>
		/// Starts a new resolution scope.
		/// </summary>
		/// <returns>The dependency scope.</returns>
		public IDependencyScope BeginScope() => new CompositionDependencyScope(HeliarCompositionProvider.Current);
	}
}