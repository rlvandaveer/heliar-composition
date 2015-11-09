using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Samples.Common
{
	public interface ILogger
	{
		void Debug(string message);
		void Warn(string message);
		void Error(string message);
	}
}