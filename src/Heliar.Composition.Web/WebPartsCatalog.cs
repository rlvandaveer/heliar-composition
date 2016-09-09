// ***********************************************************************
// Assembly         : Heliar.Composition.Web
// Author           : Robb
// Created          : 10-15-2015
//
// Last Modified By : Robb
// Last Modified On : 10-23-2015
// ***********************************************************************
// <copyright file="WebPartsCatalog.cs" company="">
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
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.ComponentModel.Composition.Registration;
using System.Linq;
using System.Reflection;

namespace Heliar.Composition.Web
{
	/// <summary>
	/// A MEF catalog for quickly wiring up web dependencies contained in a namespace named "Parts". Class cannot be inherited.
	/// </summary>
	/// <seealso cref="System.ComponentModel.Composition.Primitives.ComposablePartCatalog" />
	public sealed class WebPartsCatalog : ComposablePartCatalog
	{
		/// <summary>
		/// The underlying catalog
		/// </summary>
		private ComposablePartCatalog catalog;

		/// <summary>
		/// Gets the part definitions that are contained in the catalog.
		/// </summary>
		/// <value>The parts.</value>
		public override IQueryable<ComposablePartDefinition> Parts => this.catalog.Parts;

		/// <summary>
		/// Initializes a new instance of the <see cref="WebPartsCatalog" /> class. Overloaded constructor allows caller to specify one or more assemblies and
		/// optionally MEF conventions for finding parts.
		/// </summary>
		/// <param name="assemblies">The assemblies.</param>
		/// <param name="reflectionContext">The reflection context.</param>
		public WebPartsCatalog(Assembly[] assemblies, ReflectionContext reflectionContext = null)
		{
			if (reflectionContext == null)
				reflectionContext = GetWebPartConventions();

			catalog = new AggregateCatalog(assemblies.Select(a => new AssemblyCatalog(a, reflectionContext)));
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="WebPartsCatalog" /> class. Default constructor will use the application's assembly and default registrations for types in a "Parts" namespace.
		/// </summary>
		public WebPartsCatalog() : this(new[] { System.Web.HttpContext.Current.GetWebApplicationAssembly() }) { }

		/// <summary>
		/// Defines conventions to find parts for the catalog.
		/// </summary>
		/// <param name="conventions">The conventions.</param>
		/// <returns>ReflectionContext.</returns>
		public static RegistrationBuilder GetWebPartConventions(RegistrationBuilder conventions = null)
		{
			if (conventions == null)
				conventions = new RegistrationBuilder();

			conventions.ForTypesMatching(IsAPart)
				.Export()
				.ExportInterfaces(t => t != typeof(IDisposable));

			conventions.ForTypesMatching(t => IsAPart(t) && t.GetCustomAttributes(typeof(ApplicationScopedAttribute), true).Any())
				.AddMetadata(ApplicationScopedAttribute.MetadataValue, true);

			return conventions;
		}

		/// <summary>
		/// Determines whether the specified type is a composable part.
		/// </summary>
		/// <param name="t">The t.</param>
		/// <returns><c>true</c> if the specified type is in the parts namespace; otherwise, <c>false</c>.</returns>
		private static bool IsAPart(Type t) => !t.Name.EndsWith("Attribute") && t.Namespace != null && t.IsInNamespace("Parts");

		/// <summary>
		/// Returns an enumerator that iterates through the catalog.
		/// </summary>
		/// <returns>An enumerator that can be used to iterate through the catalog.</returns>
		public override IEnumerator<ComposablePartDefinition> GetEnumerator()
		{
			return this.catalog.GetEnumerator();
		}

		/// <summary>
		/// Releases the unmanaged resources used by the <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePartCatalog" /> and optionally releases the managed resources.
		/// </summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
				this.catalog.Dispose();

			base.Dispose(disposing);
		}
	}
}