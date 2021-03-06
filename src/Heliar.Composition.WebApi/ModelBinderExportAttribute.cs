﻿// ***********************************************************************
// Assembly         : Heliar.Composition.WebApi
// Author           : R. L. Vandaveer
// Created          : 11-12-2015
//
// Last Modified By : R. L. Vandaveer
// Last Modified On : 11-12-2015
// ***********************************************************************
// <copyright file="ModelBinderExportAttribute.cs" company="">
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
using System.Web.Http.ModelBinding;

namespace Heliar.Composition.WebApi
{
	/// <summary>
	/// MEF attribute that must be used to specify that a model binder be exported from a MEF CompositionContainer.
	/// </summary>
	/// <seealso cref="System.ComponentModel.Composition.ExportAttribute" />
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Field)]
	public class ModelBinderExportAttribute : ExportAttribute
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ModelBinderExportAttribute" /> class.
		/// </summary>
		/// <param name="modelType">The model type the binder works on.</param>
		public ModelBinderExportAttribute(Type modelType)
			: base(CompositionScopedModelBinderProvider.GetModelBinderContractName(modelType), typeof(IModelBinder)) { }
	}
}