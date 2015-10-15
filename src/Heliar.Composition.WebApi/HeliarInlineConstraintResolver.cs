using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Web.Http.Routing;

using Heliar.Composition.Web;

namespace Heliar.ComponentModel.Composition.Web.Http
{
	/// <summary>
	/// Resolves constraints and their dependencies for attributed routes.
	/// </summary>
	public class HeliarInlineConstraintResolver : IInlineConstraintResolver
	{
		/// <summary>
		/// Gets the constraint map.
		/// </summary>
		/// <value>The constraint map.</value>
		public IDictionary<string, Type> ConstraintMap { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="HeliarInlineConstraintResolver" /> class.
		/// </summary>
		public HeliarInlineConstraintResolver()
		{
			var defaultResolver = new DefaultInlineConstraintResolver();
			this.ConstraintMap = new Dictionary<string, Type>(defaultResolver.ConstraintMap);
			HeliarCompositionProvider.ApplicationScopedContainer.ComposeParts(this.ConstraintMap.Values);
		}

		/// <summary>
		/// Resolves the constraint and its dependencies.
		/// </summary>
		/// <param name="inlineConstraint">The inline constraint.</param>
		/// <returns>IHttpRouteConstraint.</returns>
		public IHttpRouteConstraint ResolveConstraint(string inlineConstraint)
		{
			Type type = this.ConstraintMap[inlineConstraint];
			return HeliarCompositionProvider.ApplicationScopedContainer.GetExportedValue<object>(AttributedModelServices.GetContractName(type)) as IHttpRouteConstraint;
		}
	}
}