using System;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.ComponentModel.Composition.Registration;
using System.Linq;
using System.Reflection;

namespace Heliar.Composition.Core
{
	/// <summary>
	/// A class that bootstraps a MEF <see cref="CompositionContainer"/> automatically in an application using types that implement <see cref="ILibraryDependencyRegistrar"/>s
	/// and <see cref="IApplicationDependencyRegistrar"/>s. This class cannot be inherited.
	/// </summary>
	/// <remarks>Use this type if you are going to build a <see cref="CompositionContainer"/> elsewhere.</remarks>
	public sealed class CatalogBootstrapper : BootstrapperBehavior, ICatalogBootstrapper
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="CatalogBootstrapper"/> class.
		/// </summary>
		public CatalogBootstrapper() : base() { }

		/// <summary>
		/// Initializes a new instance of the <see cref="BootstrapperBehavior" /> class.
		/// </summary>
		/// <param name="assemblyNamingConvention">The assembly naming convention to use to find assemblies.</param>
		/// <param name="catalogs">An optional parameter list of pre-wired-up <see cref="ComposablePartCatalog" />s.</param>
		public CatalogBootstrapper(string assemblyNamingConvention, params ComposablePartCatalog[] catalogs) : base(assemblyNamingConvention, catalogs) { }

		/// <summary>
		/// Bootstraps an <see cref="AggregateCatalog" /> by convention and/or specified params.
		/// </summary>
		/// <param name="assemblies">The assemblies that contain the desired dependencies.</param>
		/// <returns>An <see cref="AggregateCatalog" /> containing dependencies.</returns>
		public AggregateCatalog Bootstrap(Action<RegistrationBuilder> registrationFinisher = null, params Assembly[] assemblies)
		{
			base.BootstrapAssemblies(assemblies);

			var registrations = new RegistrationBuilder();

			using (var container = new CompositionContainer(this.Catalog, CompositionOptions.DisableSilentRejection))
			{
				var bootstrapExports = container.GetExports<ILibraryDependencyRegistrar>();

				foreach (var bootstrapExport in bootstrapExports)
				{
					bootstrapExport.Value.Register(registrations);
				}

				//TODO: RLV - Reconsider whether requiring application registrar is best approach
				var appBootstrapperExports = container.GetExports<IApplicationDependencyRegistrar>();
				try
				{
					var appBootstrapper = appBootstrapperExports.Single().Value;
					appBootstrapper.Register(registrations);
				}
				catch (Exception)
				{
					throw new ApplicationDependencyRegistrarImplementationException(appBootstrapperExports.Count());
				}
			}

			// Allow caller to complete any last minute registrations
			registrationFinisher?.Invoke(registrations);

			this.Catalog.Catalogs.Add(new ApplicationCatalog(registrations));
			return this.Catalog;
		}
	}
}