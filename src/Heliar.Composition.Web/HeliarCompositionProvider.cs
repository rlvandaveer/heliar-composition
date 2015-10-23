using System;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Web;

using Heliar.Composition.Core;
using System.Reflection;

namespace Heliar.Composition.Web
{
	/// <summary>
	/// Class that provides composition services to a web application via two composition scopes. The first, application scope, provides a container to be used to satisfy dependencies
	/// at an application level. The second, request scope, provides a container per web request. The container and its dependencies are torn down at the end of each web request.
	/// This class cannot be inherited.
	/// </summary>
	public sealed class HeliarCompositionProvider
	{
		/// <summary>
		/// The request scoped catalog
		/// </summary>
		private static ComposablePartCatalog RequestScopedCatalog = null;

		/// <summary>
		/// Gets the <see cref="CompositionContainer"/> scoped to the application. This container and its dependencies will be torn down
		/// when the application shuts down.
		/// </summary>
		/// <value>The application scoped container.</value>
		public static CompositionContainer ApplicationScopedContainer { get; private set; }

		/// <summary>
		/// Gets assembly of the executing web application.
		/// </summary>
		/// <value><see cref="Assembly"/> of the executing web application.</value>
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
		/// <value>The current.</value>
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
		public HeliarCompositionProvider(ICatalogBootstrapper catalogBootstrapper, IDependencyResolutionBootstrapper resolutionBootstrapper)
		{
			if (catalogBootstrapper == null) throw new ArgumentNullException(nameof(catalogBootstrapper));
			if (resolutionBootstrapper == null) throw new ArgumentNullException(nameof(resolutionBootstrapper));

			var catalog = catalogBootstrapper.Bootstrap();
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