// ***********************************************************************
// Assembly         : Heliar.Composition.Web
// Author           : R. L. Vandaveer
// Created          : 10-14-2015
//
// Last Modified By : R. L. Vandaveer
// Last Modified On : 10-14-2015
// ***********************************************************************
// <copyright file="HeliarCompositionProvider.cs">
//     Copyright (c)2013 - 2015 R. L. Vandaveer
// </copyright>
// ***********************************************************************
using Heliar.Composition.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.ComponentModel.Composition.Registration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Heliar.Composition.Web
{
	/// <summary>
	/// Class that provides composition services to a web application. This class cannot be inherited.
	/// </summary>
	public sealed class HeliarCompositionProvider
	{
		/// <summary>
		/// The request scoped catalog
		/// </summary>
		private static ComposablePartCatalog RequestScopedCatalog = null;

		/// <summary>
		/// Gets the application scoped container.
		/// </summary>
		/// <value>The application scoped container.</value>
		public static CompositionContainer ApplicationScopedContainer { get; private set; }

		/// <summary>
		/// Gets a value indicating whether the provider has been initialized.
		/// </summary>
		/// <value><c>true</c> if the provider is initialized; otherwise, <c>false</c>.</value>
		public static bool IsInitialized => ApplicationScopedContainer != null;

		/// <summary>
		/// Gets the composition container for the current scope.
		/// </summary>
		/// <value>The current.</value>
		public static CompositionContainer Current
		{
			get
			{
				return CurrentInitializedScope ??
					(CurrentInitializedScope = new CompositionContainer(RequestScopedCatalog, CompositionOptions.DisableSilentRejection | CompositionOptions.IsThreadSafe,
																		ApplicationScopedContainer));
			}
		}

		/// <summary>
		/// Gets the initialized composition container for the current scope.
		/// </summary>
		/// <value>The current initialized scope.</value>
		public static CompositionContainer CurrentInitializedScope
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
		/// <param name="bootstrapper">A catalog bootstrapper.</param>
		/// <param name="resolverBootstrapper">The dependency resolver bootstrapper.</param>
		public HeliarCompositionProvider(ICatalogBootstrapper bootstrapper, IDependencyResolverBootstrapper resolverBootstrapper)
		{
			if (bootstrapper == null)
				bootstrapper = new CatalogBootstrapper();

			if (resolverBootstrapper == null)
				resolverBootstrapper = new DependencyResolverBootstrapper();

			var catalog = bootstrapper.Bootstrap();
			var globals = catalog.Filter(cpd => cpd.ContainsPartMetadata(Constants.ApplicationShared, true)).IncludeDependencies();
			ApplicationScopedContainer = new CompositionContainer(globals, CompositionOptions.DisableSilentRejection | CompositionOptions.IsThreadSafe);
			RequestScopedCatalog = globals.Complement;

			resolverBootstrapper.Bootstrap();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="HeliarCompositionProvider" /> class.
		/// </summary>
		public HeliarCompositionProvider() : this(null, null) { }
	}
}