using System;
using System.ComponentModel.Composition;
using System.Web.Mvc;

namespace Heliar.Composition.Mvc
{
	/// <summary>
	/// Used to specify that a model binder be exported from a MEF CompositionContainer".
	/// </summary>
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Field)]
	public class ModelBinderExportAttribute : ExportAttribute
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ModelBinderExportAttribute"/> class.
		/// </summary>
		/// <param name="modelType">Type of the model binder.</param>
		public ModelBinderExportAttribute(Type modelType)
			: base(CompositionScopedModelBinderProvider.GetModelBinderContractName(modelType), typeof(IModelBinder)) { }
	}
}