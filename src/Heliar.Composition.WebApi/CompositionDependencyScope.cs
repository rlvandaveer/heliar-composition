// ***********************************************************************
// Assembly         : Heliar.Composition.WebApi
// Author           : R. L. Vandaveer
// Created          : 10-15-2015
//
// Last Modified By : R. L. Vandaveer
// Last Modified On : 10-15-2015
// ***********************************************************************
// <copyright file="CompositionDependencyScope.cs" company="">
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
using System.ComponentModel.Composition.Hosting;
using System.Web.Http.Dependencies;

using Heliar.Composition.Web;

namespace Heliar.Composition.WebApi
{
	/// <summary>
	/// Class responsible for composing dependencies scoped to a single web request.
	/// </summary>
	/// <seealso cref="System.Web.Http.Dependencies.IDependencyScope" />
	public class CompositionDependencyScope : IDependencyScope
	{
		/// <summary>
		/// The composition container
		/// </summary>
		private CompositionContainer container;

		/// <summary>
		/// Initializes a new instance of the <see cref="CompositionDependencyScope" /> class.
		/// </summary>
		/// <param name="container">The container.</param>
		public CompositionDependencyScope(CompositionContainer container)
		{
			this.container = container;
		}

		/// <summary>
		/// Retrieves a service from the scoped composition container.
		/// </summary>
		/// <param name="serviceType">The <see cref="Type" /> to retrieve an instance of.</param>
		/// <returns>The retrieved service.</returns>
		public object GetService(Type serviceType)
		{
			return this.container.GetExportedValueOrDefault<object>(AttributedModelServices.GetContractName(serviceType));
		}

		/// <summary>
		/// Retrieves a collection of services from the scoped composition container.
		/// </summary>
		/// <param name="serviceType">The collection of <see cref="Type" />s to retrieve instances of.</param>
		/// <returns>The retrieved collection of services.</returns>
		public IEnumerable<object> GetServices(Type serviceType)
		{
			return this.container.GetExportedValues<object>(AttributedModelServices.GetContractName(serviceType));
		}

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		public void Dispose()
		{
			Dispose(true);
		}

		/// <summary>
		/// Releases unmanaged and - optionally - managed resources.
		/// </summary>
		/// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
		protected void Dispose(bool disposing)
		{
			if (disposing)
			{
				CompositionScopeDisposer.DisposeCompositionScope();
			}
		}
	}
}