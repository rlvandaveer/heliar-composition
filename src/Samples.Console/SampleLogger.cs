using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Samples.Common;

namespace Samples.ConsoleApp
{
	public class SampleLogger : ILogger
	{
		public void Debug(string message)
		{
			Console.WriteLine(message);
		}

		public void Error(string message)
		{
			Console.WriteLine(message);
		}

		public void Warn(string message)
		{
			Console.WriteLine(message);
		}
	}
}