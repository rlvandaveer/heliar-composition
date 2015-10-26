using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heliar.Composition.Core.Tests
{
	[ExcludeFromCodeCoverage]
	class TestBootstrapperBehavior : BootstrapperBehavior
	{
		public new AggregateCatalog Catalog => base.Catalog;

		public TestBootstrapperBehavior(string assemblyNamingConvention, params ComposablePartCatalog[] catalogs) : base(assemblyNamingConvention, catalogs) { }
	}
}