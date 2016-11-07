// ***********************************************************************
// Assembly         : Heliar.Composition.Core
// Author           : R. L. Vandaveer
// Created          : 10-15-2015
//
// Last Modified By : R. L. Vandaveer
// Last Modified On : 08-24-2016
// ***********************************************************************
// <copyright file="CatalogBootstrapper.cs" company="">
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
	/// <seealso cref="Heliar.Composition.Core.ICatalogBootstrapper" />
	/// <remarks>Use this type if you are going to build a <see cref="CompositionContainer" /> elsewhere.</remarks>
	public sealed class CatalogBootstrapper : BootstrapperBehavior, ICatalogBootstrapper
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="CatalogBootstrapper" /> class.
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
		/// <param name="registrationFinisher">The registration finisher.</param>
		/// <param name="assemblies">The assemblies that contain the desired dependencies.</param>
		/// <returns>An <see cref="AggregateCatalog" /> containing dependencies.</returns>
		/// <exception cref="Heliar.Composition.Core.ApplicationDependencyRegistrarImplementationException"></exception>
		public AggregateCatalog Bootstrap(Action<RegistrationBuilder> registrationFinisher = null, params Assembly[] assemblies)
		{
			base.BootstrapAssemblies(assemblies);

			var registrations = new RegistrationBuilder();

			using (var container = new CompositionContainer(this.Catalog, CompositionOptions.DisableSilentRejection))
			{
				var bootstrapExports = container.GetExports<ILibraryDependencyRegistrar>();

				foreach (var bootstrapExport in bootstrapExports)
				{
					bootstrapExport.Value.Register(registrations, this.Catalog);
				}

				var appBootstrapperExports = container.GetExports<IApplicationDependencyRegistrar>();
				try
				{
					var appBootstrapper = appBootstrapperExports.Single().Value;
					appBootstrapper.Register(registrations, this.Catalog);
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