using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Primitives;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heliar.Composition.Core
{
	/// <summary>
	/// Interface that defines how a bootstrapper should behave.
	/// </summary>
	public interface IBootstrapperBehavior
	{
		/// <summary>
		/// Gets a value indicating whether the boostrapper should find assemblies of dependencies using a naming convention or whether it should find them
		/// using the application's directory path.
		/// </summary>
		/// <value><c>true</c> if using an assembly naming convention; otherwise, <c>false</c>.</value>
		bool UseAssemblyNamingConvention { get; }

		/// <summary>
		/// Gets or sets a value that should be used to locate assemblies for dependency detection.
		/// </summary>
		/// <value>The assembly naming convention.</value>
		string AssemblyNamingConvention { get; set; }

		/// <summary>
		/// Manually adds a part catalog to the bootstrapper.
		/// </summary>
		/// <param name="catalog">The catalog add.</param>
		void AddCatalog(ComposablePartCatalog catalog);
	}
}
