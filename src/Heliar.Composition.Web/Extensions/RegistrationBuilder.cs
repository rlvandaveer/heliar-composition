using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Registration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heliar.Composition.Web
{
	public static class RegistrationBuilderExtensions
	{
		public static void ForApplicationShared(this RegistrationBuilder conventions)
		{
			conventions.ForTypesMatching(t => t.GetCustomAttributes(typeof(ApplicationSharedAttribute), true).Any())
				.AddMetadata(ApplicationSharedAttribute.MetadataValue, true);
		}
	}
}