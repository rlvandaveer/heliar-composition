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
		/// <returns>The <see cref="ComposablePartDefinition" /> contained in the <see cref="ComposablePartCatalog" />.</returns>
		public override IQueryable<ComposablePartDefinition> Parts => this.catalog.Parts;

		/// <summary>
		/// Initializes a new instance of the <see cref="WebPartsCatalog"/> class. Overloaded constructor allows caller to specify one or more assemblies and
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
		/// <returns>ReflectionContext.</returns>
		public static RegistrationBuilder GetWebPartConventions(RegistrationBuilder conventions = null)
		{
			if (conventions == null)
				conventions = new RegistrationBuilder();

			conventions.ForTypesMatching(IsAPart)
				.Export()
				.ExportInterfaces(t => t != typeof(IDisposable));

			conventions.ForTypesMatching(t => IsAPart(t) && t.GetCustomAttributes(typeof(ApplicationSharedAttribute), true).Any())
				.AddMetadata(ApplicationSharedAttribute.MetadataValue, true);

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