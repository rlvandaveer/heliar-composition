// ***********************************************************************
// Assembly         : Heliar.Composition.Mvc
// Author           : R. L. Vandaveer
// Created          : 10-23-2015
//
// Last Modified By : R. L. Vandaveer
// Last Modified On : 11-12-2015
// ***********************************************************************
// <copyright file="MvcDependencyResolutionConfigurator.cs" company="">
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
using System.Linq;
using System.Web.Mvc;

using Heliar.Composition.Web;

namespace Heliar.Composition.Mvc
{
	/// <summary>
	/// Class that configures dependency resolution for MVC.
	/// </summary>
	/// <seealso cref="Heliar.Composition.Web.IDependencyResolutionConfigurator" />
	public class MvcDependencyResolutionConfigurator : IDependencyResolutionConfigurator
	{
		/// <summary>
		/// Configures dependency resolution.
		/// </summary>
		public void Configure()
		{
			this.SetResolver();
			this.SetFilterProviders();
			this.SetModelBinderProviders();
		}

		/// <summary>
		/// Sets the model binder providers.
		/// </summary>
		private void SetModelBinderProviders()
		{
			ModelBinderProviders.BinderProviders.Add(new CompositionScopedModelBinderProvider());
		}

		/// <summary>
		/// Sets the filter providers.
		/// </summary>
		private void SetFilterProviders()
		{
			FilterProviders.Providers.Remove(FilterProviders.Providers.OfType<FilterAttributeFilterProvider>().SingleOrDefault());
			FilterProviders.Providers.Add(new CompositionScopedFilterAttributeFilterProvider());
		}

		/// <summary>
		/// Sets the dependency resolver for MVC.
		/// </summary>
		private void SetResolver()
		{
			var resolver = new CompositionScopedDependencyResolver();
			DependencyResolver.SetResolver(resolver);
		}
	}
}