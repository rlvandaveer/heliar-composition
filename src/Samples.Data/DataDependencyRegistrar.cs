﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Heliar.Composition.Core;

namespace Samples.Data
{
	[ExcludeFromCodeCoverage]
	public class DataDependencyRegistrar : ILibraryDependencyRegistrar
	{
		public void Register(AggregateCatalog catalog)
		{
			//throw new NotImplementedException();
		}
	}
}