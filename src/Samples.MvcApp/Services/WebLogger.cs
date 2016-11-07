using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Samples.Common;

namespace Samples.MvcApp.Services
{
	public class WebLogger : ILogger
	{
		public void Debug(string message)
		{
			Console.WriteLine(message);
		}

		public void Warn(string message)
		{
			Console.WriteLine(message);
		}

		public void Error(string message)
		{
			Console.WriteLine(message);
		}
	}
}