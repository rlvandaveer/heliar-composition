// ***********************************************************************
// Assembly         : Heliar.Composition.WebApi
// Author           : R. L. Vandaveer
// Created          : 11-11-2015
//
// Last Modified By : R. L. Vandaveer
// Last Modified On : 11-13-2015
// ***********************************************************************
// <copyright file="HeliarModelBinderProvider.cs" company="">
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
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.ModelBinding;

namespace Heliar.Composition.WebApi
{
	/// <summary>
	/// Class HeliarModelBinderProvider. This class cannot be inherited.
	/// </summary>
	/// <seealso cref="System.Web.Http.ModelBinding.ModelBinderProvider" />
	sealed class HeliarModelBinderProvider : ModelBinderProvider
	{
		/// <summary>
		/// Gets the binders.
		/// </summary>
		/// <value>The binders.</value>
		HashSet<Lazy<IModelBinder, IModelBinderMetadata>> binders { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="HeliarModelBinderProvider"/> class.
		/// </summary>
		/// <param name="binders">The binders.</param>
		public HeliarModelBinderProvider(IEnumerable<Lazy<IModelBinder, IModelBinderMetadata>> binders)
		{
			this.binders = new HashSet<Lazy<IModelBinder, IModelBinderMetadata>>(binders, new ModelBinderComparer());
		}

		/// <summary>
		/// Finds a binder for the given type.
		/// </summary>
		/// <param name="configuration">A configuration object.</param>
		/// <param name="modelType">The type of the model to bind against.</param>
		/// <returns>A binder, which can attempt to bind this type. Or null if the binder knows statically that it will never be able to bind the type.</returns>
		public override IModelBinder GetBinder(HttpConfiguration configuration, Type modelType)
		{
			return this.binders.FirstOrDefault(b => b.Metadata.ModelType == modelType)?.Value;
		}
	}

	/// <summary>
	/// Class ModelBinderComparer.
	/// </summary>
	/// <seealso cref="System.Collections.Generic.IEqualityComparer{System.Lazy{System.Web.Http.ModelBinding.IModelBinder, Heliar.Composition.WebApi.IModelBinderMetadata}}" />
	class ModelBinderComparer : IEqualityComparer<Lazy<IModelBinder, IModelBinderMetadata>>
	{
		/// <summary>
		/// Determines whether the specified objects are equal.
		/// </summary>
		/// <param name="x">The first object of type <paramref name="T" /> to compare.</param>
		/// <param name="y">The second object of type <paramref name="T" /> to compare.</param>
		/// <returns>true if the specified objects are equal; otherwise, false.</returns>
		public bool Equals(Lazy<IModelBinder, IModelBinderMetadata> x, Lazy<IModelBinder, IModelBinderMetadata> y)
		{
			return x.Metadata.ModelType == y.Metadata.ModelType;
		}

		/// <summary>
		/// Returns a hash code for this instance.
		/// </summary>
		/// <param name="obj">The <see cref="T:System.Object" /> for which a hash code is to be returned.</param>
		/// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
		public int GetHashCode(Lazy<IModelBinder, IModelBinderMetadata> obj)
		{
			int hash = 13;
			hash = hash * 19 + obj.Metadata.ModelType.GetHashCode();
			return hash;
		}
	}

}
