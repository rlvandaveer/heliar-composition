using System.IO;

namespace System.Reflection
{
	/// <summary>
	/// Provides extensions to the <see cref="Assembly"/> class.
	/// </summary>
	public static class AssemblyExtensions
	{
		/// <summary>
		/// Gets the code base directory.
		/// </summary>
		/// <param name="assembly">The assembly.</param>
		/// <returns>System.String.</returns>
		public static string GetCodeBaseDirectory(this Assembly assembly)
		{
			string codeBase = Assembly.GetExecutingAssembly().CodeBase;
			UriBuilder uri = new UriBuilder(codeBase);
			string path = Uri.UnescapeDataString(uri.Path);
			return Path.GetDirectoryName(path);
		}
	}
}