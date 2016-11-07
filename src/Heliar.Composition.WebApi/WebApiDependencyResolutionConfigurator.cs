// ***********************************************************************
// Assembly         : Heliar.Composition.WebApi
// Author           : R. L. Vandaveer
// Created          : 10-15-2015
//
// Last Modified By : R. L. Vandaveer
// Last Modified On : 12-07-2015
// ***********************************************************************
// <copyright file="WebApiDependencyResolutionConfigurator.cs" company="">
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
using System.Web.Http;
using System.Web.Http.Filters;
using System.Web.Http.ModelBinding;
using System.Web.Http.Routing;

using Heliar.Composition.Web;

namespace Heliar.Composition.WebApi
{
	/// <summary>
	/// Class that configures the types that resolve dependencies within Web API.
	/// </summary>
	/// <seealso cref="Heliar.Composition.Web.IDependencyResolutionConfigurator" />
	public class WebApiDependencyResolutionConfigurator : IDependencyResolutionConfigurator
	{
		/// <summary>
		/// The configuration
		/// </summary>
		HttpConfiguration config = GlobalConfiguration.Configuration;

		/// <summary>
		/// Configures WebAPI dependency resolution.
		/// </summary>
		public void Configure()
		{
			this.SetFilterProvider();
			this.SetInlineConstraintResolver();
			this.SetModelBinderProvider();
			this.SetResolver();
		}

		/// <summary>
		/// Sets the filter provider.
		/// </summary>
		private void SetFilterProvider()
		{
			var providers = config.Services.GetFilterProviders().ToList();
			var defaultProvider = providers.Single(p => p is ActionDescriptorFilterProvider);
			config.Services.Remove(typeof(IFilterProvider), defaultProvider);
			config.Services.Add(typeof(IFilterProvider), new CompositionScopedFilterProvider());
		}

		/// <summary>
		/// Sets the inline constraint resolver.
		/// </summary>
		private void SetInlineConstraintResolver()
		{
			config.MapHttpAttributeRoutes(HeliarCompositionProvider.ApplicationScopedContainer.GetExportedValue<HeliarInlineConstraintResolver>());
		}

		/// <summary>
		/// Sets the model binder provider.
		/// </summary>
		private void SetModelBinderProvider()
		{
			config.Services.Add(typeof(ModelBinderProvider), HeliarCompositionProvider.ApplicationScopedContainer.GetExportedValue<HeliarModelBinderProvider>());
		}

		/// <summary>
		/// Sets the resolver.
		/// </summary>
		private void SetResolver()
		{
			var resolver = new CompositionScopedDependencyResolver();
			config.DependencyResolver = resolver;
		}
	}
}