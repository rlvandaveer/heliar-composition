using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Samples.Data
{
	public class SampleConnectionFactory : IConnectionFactory
	{
		public IDbConnection Get()
		{
			return new SampleDbConnection();
		}
	}

	public class SampleDbConnection : IDbConnection
	{
		public string ConnectionString { get; set; }

		public int ConnectionTimeout => 500;

		public string Database { get; private set; }

		public ConnectionState State { get; private set; }

		public IDbTransaction BeginTransaction()
		{
			throw new NotImplementedException();
		}

		public IDbTransaction BeginTransaction(IsolationLevel il)
		{
			throw new NotImplementedException();
		}

		public void ChangeDatabase(string databaseName)
		{
			this.Database = databaseName;
		}

		public void Close()
		{
			this.State = ConnectionState.Closed;
		}

		public IDbCommand CreateCommand()
		{
			throw new NotImplementedException();
		}

		public void Dispose()
		{
			throw new NotImplementedException();
		}

		public void Open()
		{
			this.State = ConnectionState.Open;
		}
	}

}
