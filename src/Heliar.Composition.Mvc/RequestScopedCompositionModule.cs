// ***********************************************************************
// Assembly         : Heliar.Composition.Mvc
// Author           : R. L. Vandaveer
// Created          : 10-19-2015
//
// Last Modified By : R. L. Vandaveer
// Last Modified On : 10-23-2015
// ***********************************************************************
// <copyright file="RequestScopedCompositionModule.cs" company="">
//	Copyright ©2015 - 2016 R. L. Vandaveer. Permission is hereby granted,
//	free of charge, to any person obtaining a copy of this software and
//	associated documentation files (the "Software"), to deal in the Software
//	without restriction, including without limitation the rights to use, copy,
//	modify, merge, publish, distribute, sublicense, and/or sell copies of the
//	Software, and to permit persons to whom the Software is furnished to do so,
//	subject to the following conditions: The above copyright notice and this
//	permission notice shall be included in all copies or substantial portions
//	of the Software.
//	THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS
//	OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//	FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//	AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//	LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
//	FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS
//	IN THE SOFTWARE.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Web;

using Microsoft.Web.Infrastructure.DynamicModuleHelper;

using Heliar.Composition.Web;

[assembly: PreApplicationStartMethod(typeof(Heliar.Composition.Mvc.RequestScopedCompositionModule), "Register")]
namespace Heliar.Composition.Mvc
{
	/// <summary>
	/// HttpModule to assist with the disposal of web request scoped dependencies with MVC.
	/// </summary>
	/// <seealso cref="System.Web.IHttpModule" />
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