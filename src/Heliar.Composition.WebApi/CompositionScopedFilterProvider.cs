﻿// ***********************************************************************
// Assembly         : Heliar.Composition.WebApi
// Author           : R. L. Vandaveer
// Created          : 10-15-2015
//
// Last Modified By : R. L. Vandaveer
// Last Modified On : 10-19-2015
// ***********************************************************************
// <copyright file="CompositionScopedFilterProvider.cs" company="">
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
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

using Heliar.Composition.Web;

namespace Heliar.Composition.WebApi
{
	/// <summary>
	/// Class uses <see cref="HeliarCompositionProvider" />'s current scope to resolve filter attributes.
	/// </summary>
	/// <seealso cref="System.Web.Http.Filters.ActionDescriptorFilterProvider" />
	/// <seealso cref="System.Web.Http.Filters.IFilterProvider" />
	public class CompositionScopedFilterProvider : ActionDescriptorFilterProvider, IFilterProvider
	{
		/// <summary>
		/// Gets the action filters.
		/// </summary>
		/// <param name="configuration">The configuration.</param>
		/// <param name="actionDescriptor">The action descriptor.</param>
		/// <returns>IEnumerable&lt;FilterInfo&gt;.</returns>
		public new IEnumerable<FilterInfo> GetFilters(HttpConfiguration configuration, HttpActionDescriptor actionDescriptor)
		{
			var filters = base.GetFilters(configuration, actionDescriptor).ToArray();
			this.ComposeFilters(filters);
			return filters;
		}

		/// <summary>
		/// Composes the filters.
		/// </summary>
		/// <param name="filters">The filters.</param>
		private void ComposeFilters(FilterInfo[] filters)
		{
			HeliarCompositionProvider.Current.ComposeParts(filters);
		}
	}
}