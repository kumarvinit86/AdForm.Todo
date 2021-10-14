using Serilog;
using System;

namespace SeriLogger.DbLogger
{
    public class DbLogger : IDbLogger, IDisposable
	{
		Initializer _initializer;

		public bool DoDebugLog { get; set; } = true;
		public bool DoDataLog { get; set; } = true;
		public bool DoErrorLog { get; set; } = true;

		public DbLogger(Initializer initializer)
		{
			_initializer = initializer;
		}

		public DbLogger() : this(Initializer.Initialize())
		{

		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="server"></param>
		/// <param name="database"></param>
		/// <param name="flags">Order of falag: 1.DoDatalog, 2.DoErrorLog, 3.DoDebugLog</param>
		/// <returns></returns>
		public DbLogger Initialize(string server, string database, Tuple<bool, bool, bool> flags, Tuple<string, string> user = null)
		{
			if (server == string.Empty || database == string.Empty)
			{
				throw new ArgumentException("Database or Server name to connect for logging not provided");
			}
			if (user == null)
			{
				user = new Tuple<string, string>("", "");
			}
			//_initializer = Initializer.Initialize();
			if (_initializer == null)
				_initializer = Initializer.Initialize();

			DoDataLog = flags.Item1;
			DoErrorLog = flags.Item2;
			DoDebugLog = flags.Item3;
			_initializer.ConnectionString(server, database, user.Item1, user.Item2);
			return this;
		}

		private ILogger CreateSerilogger()
		{
			if (_initializer == null)
				throw new Exception("Logger is not initialized");

			return _initializer.CreateLoggerConfiguration().CreateLogger();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="messageTemplate">Message to put into the log</param>
		/// <param name="propertyValue">Array of values which need to put into log fields as well as in message</param>
		public void Warning(string messageTemplate, params object[] propertyValue)
		{
			if (DoDataLog)
			{
				Log.Logger = CreateSerilogger();
				Log.Warning(messageTemplate, propertyValue);
				Log.CloseAndFlush();
			}
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="messageTemplate">Message to put into the log</param>
		public void Warning(string messageTemplate)
		{
			if (DoDataLog)
			{
				Log.Logger = CreateSerilogger();
				Log.Warning(messageTemplate);
				Log.CloseAndFlush();
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="messageTemplate">Message to put into the log</param>
		public void Debug(string messageTemplate)
		{
			if (DoDebugLog)
			{
				Log.Logger = CreateSerilogger();
				Log.Debug(messageTemplate);
				Log.CloseAndFlush();
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="exception">Exception /Error</param>
		/// <param name="messageTemplate">Message to put into the log</param>
		public void Error(Exception exception, string messageTemplate = "")
		{
			if (DoErrorLog)
			{
				Log.Logger = CreateSerilogger();
				Log.Error(exception, messageTemplate);
				Log.CloseAndFlush();
			}
		}



		// Dispose() calls Dispose(true)
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				//I think this is not required, but keeping it here for future change
				_initializer = null;
			}
		}
	}
}
