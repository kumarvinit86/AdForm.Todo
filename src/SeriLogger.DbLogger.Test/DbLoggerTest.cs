using System;
using Xunit;

namespace SeriLogger.DbLogger.Test
{
    public class DbLoggerTest
	{
		private const string Database = "xxxxxx";
		private const string Server = "xxxxxx";

		[Fact]
		public void Log_Debug()
		{
			// Arrange			
			var flags = new Tuple<bool, bool, bool>(false, false, false);
			DbLogger dbLogger = new DbLogger().Initialize(Server, Database, flags);

			// Act
			dbLogger.Debug("Test message");

			// Assert
			Assert.True(true);
		}

		[Fact]
		public void Log_Debug_Server_and_Database_are_blank()
		{
			// Arrange & Act & Assert						
			var flags = new Tuple<bool, bool, bool>(false, false, false);
			Assert.Throws<ArgumentException>(() => new DbLogger().Initialize(string.Empty, string.Empty, flags));
		}

		[Fact]
		public void Log_Warning()
		{
			// Arrange			
			var flags = new Tuple<bool, bool, bool>(false, false, false);
			DbLogger dbLogger = new DbLogger().Initialize(Server, Database, flags);

			// Act
			dbLogger.Warning("Test warning");

			// Assert
			Assert.True(true);
		}

		[Fact]
		public void Log_Warning_with_Argument()
		{
			// Arrange		
			var flags = new Tuple<bool, bool, bool>(false, false, false);
			DbLogger dbLogger = new DbLogger().Initialize(Server, Database, flags);

			// Act
			dbLogger.Warning("Test Warning by {user}", "Test User");

			// Assert
			Assert.True(true);
		}

		[Fact]
		public void Log_Error()
		{
			// Arrange			
			var flags = new Tuple<bool, bool, bool>(false, false, false);
			DbLogger dbLogger = new DbLogger().Initialize(Server, Database, flags);

			// Act
			dbLogger.Error(new Exception("Test exception"), "Test Error message");

			// Assert
			Assert.True(true);
		}
	}
}
