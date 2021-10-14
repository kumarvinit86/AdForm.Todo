using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using System.Collections.ObjectModel;
using System.Data;

namespace SeriLogger.DbLogger
{
    public class Initializer
	{
		private string _connectionString;
		private const string _tableName = "AdformLogs";

		private Initializer()
		{

		}

		public void ConnectionString(string server, string database, string user, string password)
		{
				_connectionString = $"data source={server};initial catalog={database};persist security info=True;" +
				(user != string.Empty ? $"user id={user};password ={password};" : "Integrated Security=True;") +
				$"Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
		}


		static Initializer _initializer;

		static object syncLock = new object();

		public static Initializer Initialize()
		{
			if (_initializer == null)
			{
				lock (syncLock)
				{
					if (_initializer == null)
					{
						_initializer = new Initializer();
					}
				}
			}
			return _initializer;
		}
		public LoggerConfiguration CreateLoggerConfiguration()
		{

			return new LoggerConfiguration()
				.Enrich.FromLogContext()
				.MinimumLevel.Debug()
				.WriteTo.MSSqlServer(
					_connectionString,
					new MSSqlServerSinkOptions
					{
						TableName = _tableName,
						AutoCreateSqlTable = true
					},
					sinkOptionsSection: null,
					appConfiguration: null,
					restrictedToMinimumLevel: LevelAlias.Minimum,
					formatProvider: null,
					columnOptions: BuildColumnOptions(),
					columnOptionsSection: null,
					logEventFormatter: null);

		}

		private ColumnOptions BuildColumnOptions()
		{
			var columnOptions = new ColumnOptions
			{
				TimeStamp =
				{
					ColumnName = "TimeStampUTC",
					ConvertToUtc = true,
				},

				AdditionalColumns = new Collection<SqlColumn>
				{
					new SqlColumn { DataType = SqlDbType.NVarChar, ColumnName = "MachineName" },
					new SqlColumn { DataType = SqlDbType.NVarChar, ColumnName = "ProcessName" },
					new SqlColumn { DataType = SqlDbType.NVarChar, ColumnName = "ThreadId" },
					new SqlColumn { DataType = SqlDbType.NVarChar, ColumnName = "CallerName" },
					new SqlColumn { DataType = SqlDbType.NVarChar, ColumnName = "SourceFile" },
					new SqlColumn { DataType = SqlDbType.NVarChar, ColumnName = "LineNumber" },
					new SqlColumn { DataType = SqlDbType.NVarChar, ColumnName = "PayLoad" }
				}
			};

			columnOptions.Store.Remove(StandardColumn.Properties);

			return columnOptions;
		}
	}
}
