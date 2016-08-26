// ***********************************************************************
// Assembly         : Heliar.Composition.Core
// Author           : R. L. Vandaveer
// Created          : 10-15-2015
//
// Last Modified By : R. L. Vandaveer
// Last Modified On : 11-12-2015
// ***********************************************************************
// <copyright file="IContainerBootstrapper.cs" company="">
//     Copyright © 2013 - 2016 R. L. Vandaveer. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Registration;
using System.Reflection;

namespace Heliar.Composition.Core
{
	/// <summary>
	/// Represents a type that knows how to bootstrap a MEF <see cref="CompositionContainer" /> using the Heliar Composition Framework.
	/// </summary>
	/// <seealso cref="Heliar.Composition.Core.IBootstrapperBehavior" />
	public interface IContainerBootstrapper : IBootstrapperBehavior
	{
		/// <summary>
		/// Wires up a <see cref="CompositionContainer" />.
		/// </summary>
		/// <param name="registrationFinisher">The registration finisher.</param>
		/// <param name="assemblies">The assemblies that contain the desired dependencies.</param>
		/// <returns>A <see cref="CompositionContainer" /> that can resolve dependencies.</returns>
		CompositionContainer Bootstrap(Action<RegistrationBuilder> registrationFinisher = null, params Assembly[] assemblies);
	}
}