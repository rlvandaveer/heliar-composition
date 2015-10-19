using System;

namespace Heliar.Composition.Web
{
	/// <summary>
	/// This attribute designates that a part's lifetime should be scoped to the application.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public class ApplicationScopedAttribute : Attribute
	{
		public const string MetadataValue = Constants.ApplicationScoped;
	}
}