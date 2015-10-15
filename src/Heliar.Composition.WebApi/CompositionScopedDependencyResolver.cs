using System.Web.Http.Dependencies;

using Heliar.Composition.Web;

namespace Heliar.Composition.WebApi
{
	/// <summary>
	/// Resolves shared or web request scoped dependencies using a composition provider.
	/// </summary>
	public class CompositionScopedDependencyResolver : CompositionDependencyScope, IDependencyResolver
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="CompositionScopedDependencyResolver"/> class.
		/// </summary>
		public CompositionScopedDependencyResolver() : base(HeliarCompositionProvider.ApplicationScopedContainer) { }

		/// <summary>
		/// Starts a new resolution scope.
		/// </summary>
		/// <returns>The dependency scope.</returns>
		public IDependencyScope BeginScope() => new CompositionDependencyScope(HeliarCompositionProvider.Current);
	}
}