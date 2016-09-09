// ***********************************************************************
// Assembly         : Heliar.Composition.Web
// Author           : Robb
// Created          : 10-15-2015
//
// Last Modified By : Robb
// Last Modified On : 08-29-2016
// ***********************************************************************
// <copyright file="HeliarCompositionProvider.cs" company="">
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
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Linq;
using System.Reflection;
using System.Web;

using Heliar.Composition.Core;

namespace Heliar.Composition.Web
{
	/// <summary>
	/// Class that provides composition services to a web application via two composition scopes. The first, application scope, provides a container to be used to satisfy dependencies
	/// at an application level. The second, request scope, provides a container per web request, ie. the container and its dependencies are torn down at the end of each web request.
	/// This class cannot be inherited.
	/// </summary>
	public sealed class HeliarCompositionProvider
	{
		/// <summary>
		/// The request scoped catalog
		/// </summary>
		private static ComposablePartCatalog RequestScopedCatalog = null;

		/// <summary>
		/// Gets the <see cref="CompositionContainer" /> scoped to the application. This container and its dependencies will be torn down
		/// when the application shuts down.
		/// </summary>
		/// <value>The application scoped container.</value>
		public static CompositionContainer ApplicationScopedContainer { get; private set; }

		/// <summary>
		/// Gets a reference to the assembly of the executing web application.
		/// </summary>
		/// <value><see cref="Assembly" /> of the executing web application.</value>
		public static Assembly WebApplicationAssembly => HttpContext.Current.GetWebApplicationAssembly();

		/// <summary>
		/// Gets a value indicating whether the provider has been initialized.
		/// </summary>
		/// <value><c>true</c> if the provider is initialized; otherwise, <c>false</c>.</value>
		public static bool IsInitialized => ApplicationScopedContainer != null;

		/// <summary>
		/// Gets the composition container for the current request scope. This container and its dependencies will be torn down
		/// when the request ends.
		/// </summary>
		/// <value>The <see cref="CompositionContainer" /> for the current web request.</value>
		public static CompositionContainer Current
		{
			get
			{
				return CurrentInitializedScope ??
					(CurrentInitializedScope = new CompositionContainer(RequestScopedCatalog,
																		CompositionOptions.DisableSilentRejection | CompositionOptions.IsThreadSafe,
																		ApplicationScopedContainer));
			}
		}

		/// <summary>
		/// Gets the initialized composition container for the current scope.
		/// </summary>
		/// <value>The current initialized scope.</value>
		internal static CompositionContainer CurrentInitializedScope
		{
			get
			{
				return (CompositionContainer)HttpContext.Current.Items[typeof(HeliarCompositionProvider)];
			}
			private set
			{
				HttpContext.Current.Items[typeof(HeliarCompositionProvider)] = value;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="HeliarCompositionProvider" /> class.
		/// </summary>
		/// <param name="catalogBootstrapper">A catalog bootstrapper.</param>
		/// <param name="resolutionBootstrapper">The dependency resolver bootstrapper.</param>
		/// <exception cref="ArgumentNullException">
		/// </exception>
		public HeliarCompositionProvider(ICatalogBootstrapper catalogBootstrapper, IDependencyResolutionBootstrapper resolutionBootstrapper)
		{
			if (catalogBootstrapper == null) throw new ArgumentNullException(nameof(catalogBootstrapper));
			if (resolutionBootstrapper == null) throw new ArgumentNullException(nameof(resolutionBootstrapper));

			var catalog = catalogBootstrapper.Bootstrap(rb => rb.ForTypesMatching(t => t.GetCustomAttributes(typeof(ApplicationScopedAttribute), true).Any()).AddMetadata(Constants.ApplicationScoped, true));
			var globals = catalog.Filter(cpd => cpd.ContainsPartMetadata(Constants.ApplicationScoped, true)).IncludeDependencies();
			ApplicationScopedContainer = new CompositionContainer(globals, CompositionOptions.DisableSilentRejection | CompositionOptions.IsThreadSafe);
			RequestScopedCatalog = globals.Complement;
			resolutionBootstrapper.Bootstrap();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="HeliarCompositionProvider" /> class.
		/// </summary>
		public HeliarCompositionProvider() : this(new CatalogBootstrapper(), new DependencyResolutionBootstrapper()) { }
	}
}