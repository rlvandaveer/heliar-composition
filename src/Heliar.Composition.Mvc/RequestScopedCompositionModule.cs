using System.Web;

using Microsoft.Web.Infrastructure.DynamicModuleHelper;

using Heliar.Composition.Web;

[assembly: PreApplicationStartMethod(typeof(Heliar.Composition.Mvc.RequestScopedCompositionModule), "Register")]
namespace Heliar.Composition.Mvc
{
	/// <summary>
	/// HttpModule to assist with the disposal of web request scoped dependencies with MVC.
	/// </summary>
	public class RequestScopedCompositionModule : IHttpModule
	{
		/// <summary>
		/// Tracks whether the module has been initialized
		/// </summary>
		private static bool isInitialized = false;

		/// <summary>
		/// Registers the HttpModule.
		/// </summary>
		public static void Register()
		{
			if (!isInitialized)
			{
				isInitialized = true;
				DynamicModuleUtility.RegisterModule(typeof(RequestScopedCompositionModule));
			}
		}

		/// <summary>
		/// Disposes of the resources (other than memory) used by the module that implements <see cref="IHttpModule" />.
		/// </summary>
		public void Dispose() { }

		/// <summary>
		/// Initializes a module and prepares it to handle requests. Wires an event handler to HttpApplication.EndRequest for disposal of
		/// the current request's CompositionContainer.
		/// </summary>
		/// <param name="context">An <see cref="HttpApplication" /> that provides access to the methods, properties, and events common to all application objects within an ASP.NET application</param>
		public void Init(HttpApplication context)
		{
			context.EndRequest += CompositionScopeDisposer.DisposeCompositionScope;
		}
	}
}