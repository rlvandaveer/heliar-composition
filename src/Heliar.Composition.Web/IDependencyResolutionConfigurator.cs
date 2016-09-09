// ***********************************************************************
// Assembly         : Heliar.Composition.Web
// Author           : Robb
// Created          : 10-15-2015
//
// Last Modified By : Robb
// Last Modified On : 09-01-2016
// ***********************************************************************
// <copyright file="IDependencyResolutionConfigurator.cs" company="">
//     Copyright (c) 2016 R. L. Vandaveer. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace Heliar.Composition.Web
{
	/// <summary>
	/// Describes a type that knows how to configure dependency resolution for a framework e.g. MVC, WebAPI.
	/// </summary>
	public interface IDependencyResolutionConfigurator
	{
		/// <summary>
		/// Configures dependency resolution for the framework.
		/// </summary>
		void Configure();
	}
}