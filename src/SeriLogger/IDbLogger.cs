using System;

namespace SeriLogger.DbLogger
{
    public interface IDbLogger
	{
		bool DoDataLog { get; set; }
		bool DoDebugLog { get; set; }
		bool DoErrorLog { get; set; }
		DbLogger Initialize(string server, string database, Tuple<bool, bool, bool> flags, Tuple<string, string> user = null);
		void Debug(string messageTemplate);
		void Error(Exception exception, string messageTemplate = "");
		void Warning(string messageTemplate);
		void Warning(string messageTemplate, params object[] propertyValue);
	}
}
