// ***********************************************************************
// Assembly         : Samples.ConsoleApp
// Author           : R. L. Vandaveer
// Created          : 11-09-2015
//
// Last Modified By : R. L. Vandaveer
// Last Modified On : 11-09-2015
// ***********************************************************************
// <copyright file="SampleLogger.cs" company="">
//     Copyright © 2015 R. L. Vandaveer
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

using Samples.Common;

namespace Samples.ConsoleApp
{
	/// <summary>
	/// An extremely simple example logging type.
	/// </summary>
	/// <seealso cref="Samples.Common.ILogger" />
	public class SampleLogger : ILogger
	{
		/// <summary>
		/// Writes the specified message to console.
		/// </summary>
		/// <param name="message">The message.</param>
		public void Debug(string message)
		{
			Console.WriteLine(message);
		}

		/// <summary>
		/// Writes the specified message to console.
		/// </summary>
		/// <param name="message">The message.</param>
		public void Error(string message)
		{
			Console.WriteLine(message);
		}

		/// <summary>
		/// Writes the specified message to console.
		/// </summary>
		/// <param name="message">The message.</param>
		public void Warn(string message)
		{
			Console.WriteLine(message);
		}
	}
}