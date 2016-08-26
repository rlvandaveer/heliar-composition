// ***********************************************************************
// Assembly         : Heliar.Composition.Core.Tests
// Author           : R. L. Vandaveer
// Created          : 10-23-2015
//
// Last Modified By : R. L. Vandaveer
// Last Modified On : 11-11-2015
// ***********************************************************************
// <copyright file="TestApplicationDependencyRegistrar.cs" company="">
//     Copyright © 2013 - 2016 R. L. Vandaveer
// </copyright>
// <summary>Dependency mocks for testing</summary>
// ***********************************************************************
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Registration;
using System.Diagnostics.CodeAnalysis;

namespace Heliar.Composition.Core.Tests
{
	/// <summary>
	/// Mock ApplicationDependencyRegistrar class.
	/// </summary>
	/// <seealso cref="Heliar.Composition.Core.IApplicationDependencyRegistrar" />
	[ExcludeFromCodeCoverage]
	class TestApplicationDependencyRegistrar : IApplicationDependencyRegistrar
	{
		/// <summary>
		/// Registers the dependencies within this application.
		/// </summary>
		/// <param name="registrations">The registrations.</param>
		public void Register(RegistrationBuilder registrations)
		{
			registrations.ForType<Foo>()
				.SetCreationPolicy(CreationPolicy.NonShared)
				.ExportInterfaces()
				.Export();
		}
	}

	/// <summary>
	/// Interface IFoo
	/// </summary>
	interface IFoo
	{
		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>The name.</value>
		string Name { get; set; }
	}

	/// <summary>
	/// Class Foo.
	/// </summary>
	/// <seealso cref="Heliar.Composition.Core.Tests.IFoo" />
	[ExcludeFromCodeCoverage]
	class Foo : IFoo
	{
		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>The name.</value>
		public string Name { get; set; }
		/// <summary>
		/// Initializes a new instance of the <see cref="Foo"/> class.
		/// </summary>
		public Foo() { this.Name = "Foo"; }
	}

	/// <summary>
	/// Class FooBar.
	/// </summary>
	/// <seealso cref="Heliar.Composition.Core.Tests.IFoo" />
	[ExcludeFromCodeCoverage]
	class FooBar : IFoo
	{
		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>The name.</value>
		public string Name { get; set; }
		/// <summary>
		/// Initializes a new instance of the <see cref="FooBar"/> class.
		/// </summary>
		public FooBar() { this.Name = "Foo Bar"; }
	}
}