// ***********************************************************************
// Assembly         : Heliar.Composition.Mvc
// Author           : R. L. Vandaveer
// Created          : 10-23-2015
//
// Last Modified By : R. L. Vandaveer
// Last Modified On : 11-12-2015
// ***********************************************************************
// <copyright file="CompositionScopedFilterAttributeFilterProvider.cs" company="">
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
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web.Mvc;

using Heliar.Composition.Web;

namespace Heliar.Composition.Mvc
{
	/// <summary>
	/// Uses a scoped composition provider to resolve filter attributes.
	/// </summary>
	/// <seealso cref="System.Web.Mvc.FilterAttributeFilterProvider" />
	internal class CompositionScopedFilterAttributeFilterProvider : FilterAttributeFilterProvider
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="CompositionScopedFilterAttributeFilterProvider" /> class.
		/// </summary>
		public CompositionScopedFilterAttributeFilterProvider() : base(cacheAttributeInstances: false) { }

		/// <summary>
		/// Gets a collection of custom action attributes.
		/// </summary>
		/// <param name="controllerContext">The controller context.</param>
		/// <param name="actionDescriptor">The action descriptor.</param>
		/// <returns>A collection of custom action attributes.</returns>
		protected override IEnumerable<FilterAttribute> GetActionAttributes(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
		{
			var attributes = base.GetActionAttributes(controllerContext, actionDescriptor).ToArray();
			this.ComposeAttributes(attributes);
			return attributes;
		}

		/// <summary>
		/// Gets a collection of controller attributes.
		/// </summary>
		/// <param name="controllerContext">The controller context.</param>
		/// <param name="actionDescriptor">The action descriptor.</param>
		/// <returns>A collection of controller attributes.</returns>
		protected override IEnumerable<FilterAttribute> GetControllerAttributes(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
		{
			var attributes = base.GetControllerAttributes(controllerContext, actionDescriptor).ToArray();
			this.ComposeAttributes(attributes);
			return attributes;
		}

		/// <summary>
		/// Composes the specified attributes.
		/// </summary>
		/// <param name="attributes">The attributes.</param>
		private void ComposeAttributes(FilterAttribute[] attributes)
		{
			HeliarCompositionProvider.Current.ComposeParts(attributes);
		}
	}
}