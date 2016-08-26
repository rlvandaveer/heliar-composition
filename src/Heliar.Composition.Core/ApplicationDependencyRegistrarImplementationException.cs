// ***********************************************************************
// Assembly         : Heliar.Composition.Core
// Author           : R. L. Vandaveer
// Created          : 10-23-2015
//
// Last Modified By : R. L. Vandaveer
// Last Modified On : 08-18-2016
// ***********************************************************************
// <copyright file="ApplicationDependencyRegistrarImplementationException.cs" company="">
//    Copyright ©2013 - 2016 R. L. Vandaveer. All rights reserved.
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