using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Registration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heliar.Composition.Web
{
	/// <summary>
	/// Class adds helper methods to <see cref="RegistrationBuilder"/>s.
	/// </summary>
	public static class RegistrationBuilderExtensions
	{
		/// <summary>
		/// Builds a MEF convention for types decorated with <see cref="ApplicationScopedAttribute"/>.
		/// </summary>
		/// <param name="conventions">The conventions.</param>
		public static void ForApplicationScoped(this RegistrationBuilder conventions)
		{
			conventions.ForTypesMatching(t => t.GetCustomAttributes(typeof(ApplicationScopedAttribute), true).Any())
				.AddMetadata(ApplicationScopedAttribute.MetadataValue, true);
		}
	}
}