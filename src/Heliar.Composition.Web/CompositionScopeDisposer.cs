// ***********************************************************************
// Assembly         : Heliar.Composition.Web
// Author           : R. L. Vandaveer
// Created          : 10-15-2015
//
// Last Modified By : R. L. Vandaveer
// Last Modified On : 08-29-2016
// ***********************************************************************
// <copyright file="CompositionScopeDisposer.cs" company="">
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
using System;

namespace Heliar.Composition.Web
{
	/// <summary>
	/// Responsible for cleaning up a composition container and its resources when they go  out of scope,
	/// i.e. at the end of a web request.
	/// </summary>
	public static class CompositionScopeDisposer
	{
		/// <summary>
		/// Disposes of the composition scope.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
		public static void DisposeCompositionScope(object sender, EventArgs e)
		{
			DisposeCompositionScope();
		}

		/// <summary>
		/// Disposes of the composition scope.
		/// </summary>
		public static void DisposeCompositionScope()
		{
			var scope = HeliarCompositionProvider.CurrentInitializedScope;
			scope?.Dispose();
		}
	}
}