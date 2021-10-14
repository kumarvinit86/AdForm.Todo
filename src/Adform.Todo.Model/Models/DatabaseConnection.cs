namespace Adform.Todo.Model.Models
{
    public class DatabaseConnection
	{
		public string Server { get; set; }
		public string Database { get; set; }
		public string UserName { get; set; }
		public string Password { get; set; }

		/// <summary>
		/// This will return database connection string.
		/// </summary>
		/// <returns>string value.</returns>
		public override string ToString()
		{
			return $"data source={Server};initial catalog={Database};persist security info=True;" +
				(UserName!=string.Empty?$"user id={UserName};password ={Password};": "Integrated Security=True;") +
				$"Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
		}

	}
}
