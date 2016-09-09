// ***********************************************************************
// Assembly         : Heliar.Composition.Core
// Author           : R. L. Vandaveer
// Created          : 10-15-2015
//
// Last Modified By : R. L. Vandaveer
// Last Modified On : 08-24-2016
// ***********************************************************************
// <copyright file="BootstrapperBehavior.cs" company="">
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
//	FROM, OUT OF OR IN
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.ComponentModel.Composition.Registration;
using System.Reflection;

namespace Heliar.Composition.Core
{
	/// <summary>
	/// Base class for types that bootstrap MEF composition.
	/// </summary>
	/// <seealso cref="Heliar.Composition.Core.IBootstrapperBehavior" />
	public abstract class BootstrapperBehavior : IBootstrapperBehavior
	{
		/// <summary>
		/// The core composable parts catalog
		/// </summary>
		protected readonly AggregateCatalog Catalog = new AggregateCatalog();

		/// <summary>
		/// Holds The MEF conventions for wiring up registrars.
		/// </summary>
		protected readonly RegistrationBuilder Conventions = new RegistrationBuilder();

		/// <summary>
		/// Initializes a new instance of the <see cref="ContainerBootstrapper" /> class. Adds dependency registrar conventions.
		/// </summary>
		protected BootstrapperBehavior()
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
		/// Initializes a new instance of the <see cref="BootstrapperBehavior" /> class. The convention must conform to <see cref="https://msdn.microsoft.com/en-us/library/wz42302f(v=vs.110).aspx" />
		/// </summary>
		/// <param name="assemblyNamingConvention">The naming convention that should be used to find assemblies of composable types.</param>
		/// <param name="catalogs">An optional parameter list of pre-wired-up <see cref="ComposablePartCatalog" />s.</param>
		protected BootstrapperBehavior(string assemblyNamingConvention, params ComposablePartCatalog[] catalogs) : this()
		{
			if (!String.IsNullOrWhiteSpace(assemblyNamingConvention))
			{
				this.AssemblyNamingConvention = assemblyNamingConvention;
			}

			foreach (var catalog in catalogs)
			{
				this.Catalog.Catalogs.Add(catalog);
			}
		}

		/// <summary>
		/// Gets a value indicating whether this instance should find and wire up assemblies of dependencies automatically by a naming convention
		/// or whether it should wire up all assemblies in the application directory path.
		/// </summary>
		/// <value><c>true</c> if this instance should wire up dependencies by convention; <c>false</c> if it should wire up all dependencies
		/// in the application directory path.</value>
		public bool UseAssemblyNamingConvention => !String.IsNullOrWhiteSpace(this.AssemblyNamingConvention);

		/// <summary>
		/// Gets or sets the assembly naming convention. The convention must conform to <see cref="https://msdn.microsoft.com/en-us/library/wz42302f(v=vs.110).aspx" />
		/// </summary>
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

		/// <summary>
		/// Bootstraps the assemblies.
		/// </summary>
		/// <param name="assemblies">The assemblies.</param>
		protected void BootstrapAssemblies(params Assembly[] assemblies)
		{
			if (this.UseAssemblyNamingConvention)
			{
				this.Catalog.Catalogs.Add(new DirectoryCatalog($"{Assembly.GetExecutingAssembly().GetCodeBaseDirectory()}", AssemblyNamingConvention, this.Conventions));
			}
			else
			{
				this.Catalog.Catalogs.Add(new ApplicationCatalog(this.Conventions));
			}

			foreach (var assembly in assemblies)
			{
				this.Catalog.Catalogs.Add(new AssemblyCatalog(assembly, this.Conventions));
			}
		}
	}
}