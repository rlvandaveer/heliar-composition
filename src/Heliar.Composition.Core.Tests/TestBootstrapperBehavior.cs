// ***********************************************************************
// Assembly         : Heliar.Composition.Core.Tests
// Author           : R. L. Vandaveer
// Created          : 10-26-2015
//
// Last Modified By : R. L. Vandaveer
// Last Modified On : 10-26-2015
// ***********************************************************************
// <copyright file="TestBootstrapperBehavior.cs" company="">
//     Copyright © R. L. Vandaveer 2015
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Diagnostics.CodeAnalysis;

namespace Heliar.Composition.Core.Tests
{
	/// <summary>
	/// Mock facilitates testing base class behavior.
	/// </summary>
	/// <seealso cref="Heliar.Composition.Core.BootstrapperBehavior" />
	[ExcludeFromCodeCoverage]
	class TestBootstrapperBehavior : BootstrapperBehavior
	{
		/// <summary>
		/// Gets the catalog.
		/// </summary>
		/// <value>The catalog.</value>
		public new AggregateCatalog Catalog => base.Catalog;

		/// <summary>
		/// Initializes a new instance of the <see cref="TestBootstrapperBehavior"/> class.
		/// </summary>
		/// <param name="assemblyNamingConvention">The naming convention that should be used to find assemblies of composable types.</param>
		/// <param name="catalogs">An optional parameter list of pre-wired-up <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePartCatalog" />s.</param>
		public TestBootstrapperBehavior(string assemblyNamingConvention, params ComposablePartCatalog[] catalogs) : base(assemblyNamingConvention, catalogs) { }
	}
}