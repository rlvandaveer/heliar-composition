// ***********************************************************************
// Assembly         : Heliar.Composition.Core
// Author           : R. L. Vandaveer
// Created          : 10-15-2015
//
// Last Modified By : R. L. Vandaveer
// Last Modified On : 11-12-2015
// ***********************************************************************
// <copyright file="ContainerBootstrapper.cs" company="">
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
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.ComponentModel.Composition.Registration;
using System.Linq;
using System.Reflection;

namespace Heliar.Composition.Core
{
	/// <summary>
	/// A class that bootstraps a MEF <see cref="CompositionContainer" /> automatically in an application using types that implement <see cref="ILibraryDependencyRegistrar" />s
	/// and <see cref="IApplicationDependencyRegistrar" />s. This class cannot be inherited.
	/// </summary>
	/// <seealso cref="Heliar.Composition.Core.BootstrapperBehavior" />
	/// <seealso cref="Heliar.Composition.Core.IContainerBootstrapper" />
	public sealed class ContainerBootstrapper : BootstrapperBehavior, IContainerBootstrapper
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ContainerBootstrapper" /> class.
		/// </summary>
		public ContainerBootstrapper() : base() { }

		/// <summary>
		/// Initializes a new instance of the <see cref="ContainerBootstrapper" /> class.
		/// </summary>
		/// <param name="assemblyNamingConvention">The assembly naming convention to use to find assemblies.</param>
		/// <param name="catalogs">An optional list of pre-wired-up catalogs.</param>
		public ContainerBootstrapper(string assemblyNamingConvention, params ComposablePartCatalog[] catalogs) : base(assemblyNamingConvention, catalogs) { }

		/// <summary>
		/// Bootstraps a <see cref="CompositionContainer" /> by convention and/or specified params.
		/// </summary>
		/// <param name="registrationFinisher">The registration finisher.</param>
		/// <param name="assemblies">An optional list of assemblies.</param>
		/// <returns>System.ComponentModel.Composition.Hosting.CompositionContainer.</returns>
		/// <exception cref="Heliar.Composition.Core.ApplicationDependencyRegistrarImplementationException"></exception>
		public CompositionContainer Bootstrap(Action<RegistrationBuilder> registrationFinisher = null, params Assembly[] assemblies)
		{
			base.BootstrapAssemblies(assemblies);

			var registrations = new RegistrationBuilder();

			using (var bootstrapContainer = new CompositionContainer(this.Catalog, CompositionOptions.DisableSilentRejection))
			{
				var bootstrapExports = bootstrapContainer.GetExports<ILibraryDependencyRegistrar>();

				foreach (var bootstrapExport in bootstrapExports)
				{
					bootstrapExport.Value.Register(registrations);
				}

				//TODO: RLV - Reconsider whether requiring application registrar is best approach
				var appBootstrapperExports = bootstrapContainer.GetExports<IApplicationDependencyRegistrar>();
				var count = appBootstrapperExports.Count();
				if (count == 1)
				{
					var appBootstrapper = appBootstrapperExports.Single().Value;
					appBootstrapper.Register(registrations);
				}
				else
				{
					throw new ApplicationDependencyRegistrarImplementationException(count);
				}
			}

			// Allow caller to complete any last minute registrations
			if (registrationFinisher != null)
				registrationFinisher(registrations);

			this.Catalog.Catalogs.Add(new ApplicationCatalog(registrations));

			return new CompositionContainer(this.Catalog, CompositionOptions.DisableSilentRejection | CompositionOptions.IsThreadSafe);
		}
	}
}