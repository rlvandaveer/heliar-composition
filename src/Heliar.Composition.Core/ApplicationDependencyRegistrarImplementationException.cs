// ***********************************************************************
// Assembly         : Heliar.Composition.Core
// Author           : R. L. Vandaveer
// Created          : 10-23-2015
//
// Last Modified By : R. L. Vandaveer
// Last Modified On : 08-18-2016
// ***********************************************************************
// <copyright file="ApplicationDependencyRegistrarImplementationException.cs" company="">
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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heliar.Composition.Core
{
	/// <summary>
	/// Exception class that denotes that an application did not implement a single <see cref="IApplicationDependencyRegistrar" /> and properly expose it to the bootstrapper.
	/// </summary>
	/// <seealso cref="System.Exception" />
	public class ApplicationDependencyRegistrarImplementationException : Exception
	{
		/// <summary>
		/// Gets the number of implementations.
		/// </summary>
		/// <value>The number of implementations.</value>
		public int NumberOfImplementations { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="ApplicationDependencyRegistrarImplementationException" /> class.
		/// </summary>
		/// <param name="numberImplemented">The number of <see cref="IApplicationDependencyRegistrar" /> implemented.</param>
		public ApplicationDependencyRegistrarImplementationException(int numberImplemented)
			: base(numberImplemented == 0 ? "No implementation of IApplicationDependencyRegistrar found" : "More than one implementation of IApplicationDependencyRegistrar found")
		{
			this.NumberOfImplementations = numberImplemented;
		}
	}
}