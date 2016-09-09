// ***********************************************************************
// Assembly         : Heliar.Composition.Mvc
// Author           : R. L. Vandaveer
// Created          : 10-19-2015
//
// Last Modified By : R. L. Vandaveer
// Last Modified On : 10-19-2015
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
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Web.Mvc;

using Heliar.Composition.Web;

namespace Heliar.Composition.Mvc
{
	/// <summary>
	/// Resolves shared or web request scoped dependencies using a composition provider.
	/// </summary>
	/// <seealso cref="System.Web.Mvc.IDependencyResolver" />
	public class CompositionScopedDependencyResolver : IDependencyResolver
	{
		/// <summary>
		/// Resolves singly registered services that support arbitrary object creation using the current
		/// composition provider.
		/// </summary>
		/// <param name="serviceType">The type of the requested service or object.</param>
		/// <returns>The requested service or object.</returns>
		public object GetService(Type serviceType)
		{
			return HeliarCompositionProvider.Current.GetExportedValueOrDefault<object>(AttributedModelServices.GetContractName(serviceType));
		}

		/// <summary>
		/// Resolves multiply registered services using the current composition provider.
		/// </summary>
		/// <param name="serviceType">The type of the requested services.</param>
		/// <returns>The requested services.</returns>
		public IEnumerable<object> GetServices(Type serviceType)
		{
			return HeliarCompositionProvider.Current.GetExportedValues<object>(AttributedModelServices.GetContractName(serviceType));
		}
	}
}