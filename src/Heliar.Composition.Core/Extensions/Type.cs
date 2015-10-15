namespace System
{
	/// <summary>
	/// Provides extensions to the <see cref="Type"/> class.
	/// </summary>
	public static class TypeExtensions
	{
		/// <summary>
		/// Determines whether the type is in a namespace identified by the specified namespace fragment.
		/// </summary>
		/// <param name="type">The type to check.</param>
		/// <param name="namespaceFragment">The namespace fragment.</param>
		/// <returns><c>true</c> if the <see cref="Type"/> is in the namespace identified by the specified namespace fragment; otherwise, <c>false</c>.</returns>
		public static bool IsInNamespace(this Type type, string namespaceFragment)
		{
			return type.Namespace != null && (type.Namespace.EndsWith("." + namespaceFragment) || type.Namespace.Contains("." + namespaceFragment + "."));
		}
	}
}