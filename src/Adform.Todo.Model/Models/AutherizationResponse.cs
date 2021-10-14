namespace Adform.Todo.Model.Models
{
    public class AutherizationResponse
    {
        public bool IsValidUser { get; set; }
        public string Message { get; set; }
        public string AuthToken { get; set; }
    }
}
