using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heliar.Composition.Core
{
	/// <summary>
	/// Exception class that denotes that an application did not implement a single <see cref="IApplicationDependencyRegistrar"/> and properly expose it to the bootstrapper.
	/// </summary>
	public class ApplicationDependencyRegistrarImplementationException : Exception
	{
		public int NumberOfImplementations { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="ApplicationDependencyRegistrarImplementationException"/> class.
		/// </summary>
		/// <param name="numberImplemented">The number of <see cref="IApplicationDependencyRegistrar"/> implemented.</param>
		public ApplicationDependencyRegistrarImplementationException(int numberImplemented)
			: base(numberImplemented == 0 ? "No implementation of IApplicationDependencyRegistrar found" : "More than one implementation of IApplicationDependencyRegistrar found")
		{
			this.NumberOfImplementations = numberImplemented;
		}
	}
}