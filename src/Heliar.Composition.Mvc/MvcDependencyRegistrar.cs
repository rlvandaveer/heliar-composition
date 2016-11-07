// ***********************************************************************
// Assembly         : Heliar.Composition.Mvc
// Author           : R. L. Vandaveer
// Created          : 10-23-2015
//
// Last Modified By : R. L. Vandaveer
// Last Modified On : 09-01-2016
// ***********************************************************************
// <copyright file="MvcDependencyRegistrar.cs" company="">
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
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Registration;
using System.Web.Mvc;

using Heliar.Composition.Core;

namespace Heliar.Composition.Mvc
{
	/// <summary>
	/// Class responsible for registering MVC dependencies.
	/// </summary>
	/// <seealso cref="Heliar.Composition.Core.ILibraryDependencyRegistrar" />
	public class MvcDependencyRegistrar : ILibraryDependencyRegistrar
	{
		/// <summary>
		/// Adds the necessary registrations to perform composition for MVC.
		/// </summary>
		/// <param name="registrations">The dependency registrations/conventions to wire up.</param>
		/// <param name="catalog">An AggregateCatalog that can be added to if dependencies reside in an external assembly, i.e. BCL.</param>
		/// <remarks>Note that many MVC types like filters are composed by
		/// custom providers and not the stock resolver and therefore aren't
		/// configured here.</remarks>
		public void Register(RegistrationBuilder registrations, AggregateCatalog catalog)
		{
			registrations.ForTypesDerivedFrom<IController>()
				.SetCreationPolicy(CreationPolicy.NonShared)
				.Export();
		}
	}
}