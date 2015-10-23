namespace Heliar.Composition.Web
{
	/// <summary>
	/// Describes a type that knows how to configure dependency resolution for a framework e.g. MVC.
	/// </summary>
	public interface IDependencyResolutionConfigurator
	{
		/// <summary>
		/// Configures dependency resolution for the framework.
		/// </summary>
		void Configure();
	}
}