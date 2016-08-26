// ***********************************************************************
// Assembly         : Heliar.Composition.Core
// Author           : R. L. Vandaveer
// Created          : 10-15-2015
//
// Last Modified By : R. L. Vandaveer
// Last Modified On : 11-11-2015
// ***********************************************************************
// <copyright file="ILibraryDependencyRegistrar.cs" company="">
//     Copyright ©2013 - 2016 R. L. Vandaveer. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.ComponentModel.Composition.Registration;

namespace Heliar.Composition.Core
{
	/// <summary>
	/// Represents a type that knows how to register the dependencies within a library.
	/// </summary>
	public interface ILibraryDependencyRegistrar
	{
		/// <summary>
		/// Bootstraps the dependencies within this library.
		/// </summary>
		/// <param name="registrations">The registrations.</param>
		void Register(RegistrationBuilder registrations);
	}
}