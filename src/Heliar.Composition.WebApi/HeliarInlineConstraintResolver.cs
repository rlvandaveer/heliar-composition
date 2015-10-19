using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web.Http.Routing;

using Heliar.Composition.Web;

namespace Heliar.ComponentModel.Composition.Web.Http
{
	/// <summary>
	/// Resolves constraints and their dependencies for attributed routes. Uses the default list of constraint resolvers that come with
	/// <see cref="DefaultInlineConstraintResolver"/> plus any wired up using MEF.
	/// </summary>
	public class HeliarInlineConstraintResolver : IInlineConstraintResolver
	{
		/// <summary>
		/// Gets the canonical constraint names.
		/// </summary>
		/// <value>The constraint names.</value>
		public IList<string> ConstraintNames { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="HeliarInlineConstraintResolver" /> class.
		/// </summary>
		public HeliarInlineConstraintResolver(IEnumerable<Lazy<IHttpRouteConstraint, IHttpRouteConstraintMetadata>> constraints)
		{
			var defaultResolver = new DefaultInlineConstraintResolver();
			this.ConstraintNames = new List<string>(defaultResolver.ConstraintMap.Select(c => AttributedModelServices.GetContractName(c.Value)));
			HeliarCompositionProvider.ApplicationScopedContainer.ComposeParts(defaultResolver.ConstraintMap);

			foreach (var constraint in constraints)
			{
				this.ConstraintNames.Add(constraint.Metadata.ConstraintName);
			}
		}

		/// <summary>
		/// Resolves the constraint and its dependencies.
		/// </summary>
		/// <param name="inlineConstraint">The inline constraint.</param>
		/// <returns>IHttpRouteConstraint.</returns>
		public IHttpRouteConstraint ResolveConstraint(string inlineConstraint)
		{
			return HeliarCompositionProvider.ApplicationScopedContainer.GetExportedValue<object>(inlineConstraint) as IHttpRouteConstraint;
		}
	}

	/// <summary>
	/// Represents metadata for <see cref="IHttpRouteConstraint"/>s. Describes implementing types so that they can be wired up by MEF and retrieved
	/// by <see cref="HeliarInlineConstraintResolver"/>.
	/// </summary>
	public interface IHttpRouteConstraintMetadata
	{
		/// <summary>
		/// Gets the canonical name of the constraint.
		/// </summary>
		/// <value>The name of the constraint.</value>
		string ConstraintName { get; }
	}
}