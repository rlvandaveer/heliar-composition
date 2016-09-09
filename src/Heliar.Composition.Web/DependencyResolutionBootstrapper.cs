// ***********************************************************************
// Assembly         : Heliar.Composition.Web
// Author           : Robb
// Created          : 10-15-2015
//
// Last Modified By : Robb
// Last Modified On : 11-11-2015
// ***********************************************************************
// <copyright file="DependencyResolutionBootstrapper.cs" company="">
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
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition;
using System.Reflection;

using Heliar.Composition.Core;
using System.ComponentModel.Composition.Registration;

namespace Heliar.Composition.Web
{
	/// <summary>
	/// Class that bootstraps all of the dependency resolvers for the current application by discovering all of the <see cref="IDependencyResolutionConfigurator" />s
	/// and executing them.
	/// </summary>
	/// <seealso cref="Heliar.Composition.Web.IDependencyResolutionBootstrapper" />
	public class DependencyResolutionBootstrapper : IDependencyResolutionBootstrapper
	{
		/// <summary>
		/// The catalog
		/// </summary>
		private readonly DirectoryCatalog catalog = null;
		/// <summary>
		/// The assembly naming convention
		/// </summary>
		const string AssemblyNamingConvention = "Heliar*.dll";

		/// <summary>
		/// Initializes a new instance of the <see cref="DependencyResolutionBootstrapper" /> class.
		/// </summary>
		public DependencyResolutionBootstrapper()
		{
			var conventions = new RegistrationBuilder();
				conventions.ForTypesDerivedFrom<IDependencyResolutionConfigurator>()
				.SetCreationPolicy(CreationPolicy.Shared)
				.ExportInterfaces()
				.Export();
			this.catalog = new DirectoryCatalog($"{Assembly.GetExecutingAssembly().GetCodeBaseDirectory()}", AssemblyNamingConvention, conventions);
		}

		/// <summary>
		/// Bootstraps the dependency resolvers for the application.
		/// </summary>
		public void Bootstrap()
		{
			using (var container = new CompositionContainer(this.catalog, CompositionOptions.DisableSilentRejection))
			{
				var configurators = container.GetExportedValues<IDependencyResolutionConfigurator>();

				foreach (var configurator in configurators)
				{
					configurator.Configure();
				}
			}
		}
	}
}