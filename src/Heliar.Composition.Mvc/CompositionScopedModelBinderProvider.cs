// ***********************************************************************
// Assembly         : Heliar.Composition.Mvc
// Author           : R. L. Vandaveer
// Created          : 10-23-2015
//
// Last Modified By : R. L. Vandaveer
// Last Modified On : 11-12-2015
// ***********************************************************************
// <copyright file="CompositionScopedModelBinderProvider.cs" company="">
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
using System.ComponentModel.Composition;
using System.Web.Mvc;

using Heliar.Composition.Web;

namespace Heliar.Composition.Mvc
{
	/// <summary>
	/// Uses a scoped composition provider to resolve model binders.
	/// </summary>
	/// <seealso cref="System.Web.Mvc.IModelBinderProvider" />
	class CompositionScopedModelBinderProvider : IModelBinderProvider
	{
		/// <summary>
		/// The model binder contract name suffix
		/// </summary>
		const string ModelBinderContractNameSuffix = "++ModelBinder";

		/// <summary>
		/// Gets the name of the model binder contract.
		/// </summary>
		/// <param name="modelType">Type of the model.</param>
		/// <returns>System.String.</returns>
		public static string GetModelBinderContractName(Type modelType)
		{
			return AttributedModelServices.GetContractName(modelType) + ModelBinderContractNameSuffix;
		}

		/// <summary>
		/// Returns the model binder for the specified type.
		/// </summary>
		/// <param name="modelType">The type of the model.</param>
		/// <returns>The model binder for the specified type.</returns>
		public IModelBinder GetBinder(Type modelType)
		{
			return HeliarCompositionProvider.Current.GetExportedValueOrDefault<IModelBinder>(GetModelBinderContractName(modelType));
		}
	}
}