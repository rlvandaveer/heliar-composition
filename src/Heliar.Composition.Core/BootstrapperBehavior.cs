using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.ComponentModel.Composition.Registration;

namespace Heliar.Composition.Core
{
	/// <summary>
	/// Base class for types that bootstrap MEF composition.
	/// </summary>
	public abstract class BootstrapperBehavior : IBootstrapperBehavior
	{
		/// <summary>
		/// The core composable parts catalog
		/// </summary>
		protected readonly AggregateCatalog Catalog = new AggregateCatalog();
		/// <summary>
		/// The MEF conventions
		/// </summary>
		protected readonly RegistrationBuilder Conventions = new RegistrationBuilder();

		/// <summary>
		/// Initializes a new instance of the <see cref="ContainerBootstrapper" /> class.
		/// </summary>
		public BootstrapperBehavior()
		{
			this.Conventions.ForTypesDerivedFrom<ILibraryDependencyRegistrar>()
				.SetCreationPolicy(CreationPolicy.Shared)
				.ExportInterfaces()
				.Export();
			this.Conventions.ForTypesDerivedFrom<IApplicationDependencyRegistrar>()
				.SetCreationPolicy(CreationPolicy.Shared)
				.ExportInterfaces()
				.Export();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="BootstrapperBehavior" /> class.
		/// </summary>
		/// <param name="assemblyNamingConvention">The assembly naming convention.</param>
		/// <exception cref="System.ArgumentNullException"></exception>
		public BootstrapperBehavior(string assemblyNamingConvention, params ComposablePartCatalog[] catalogs) : this()
		{
			if (String.IsNullOrWhiteSpace(assemblyNamingConvention)) throw new ArgumentNullException(nameof(assemblyNamingConvention));

			this.AssemblyNamingConvention = assemblyNamingConvention;

			foreach (var catalog in catalogs)
			{
				this.Catalog.Catalogs.Add(catalog);
			}
		}

		/// <summary>
		/// Gets a value indicating whether this instance should find and wire up assemblies of dependencies automatically by convention.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance should wire up dependencies by convention; otherwise, <c>false</c>.</value>
		public bool UseAssemblyNamingConvention => !String.IsNullOrWhiteSpace(this.AssemblyNamingConvention);

		/// <summary>
		/// Gets or sets the assembly naming convention. The convention must conform to <see cref="https://msdn.microsoft.com/en-us/library/wz42302f(v=vs.110).aspx" /></summary>
		/// <value>The assembly naming convention.</value>
		public string AssemblyNamingConvention { get; set; }

		/// <summary>
		/// Adds a catalog to be bootstrapped.
		/// </summary>
		/// <param name="catalog">The catalog to add.</param>
		public void AddCatalog(ComposablePartCatalog catalog)
		{
			this.Catalog.Catalogs.Add(catalog);
		}
	}
}