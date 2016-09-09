// ***********************************************************************
// Assembly         : Heliar.Composition.Core
// Author           : R. L. Vandaveer
// Created          : 10-15-2015
//
// Last Modified By : R. L. Vandaveer
// Last Modified On : 11-09-2015
// ***********************************************************************
// <copyright file="IBootstrapperBehavior.cs" company="">
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
using System.ComponentModel.Composition.Primitives;

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
