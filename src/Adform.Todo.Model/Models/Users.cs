namespace Adform.Todo.Model.Models
{
    public class Users:IUser
    {
        public string UserId { get; set; }
        public string Password { get; set; }
        public bool IsAuthenticated { get; set; }
        public string JwtBearerToken { get; set; }
        public bool IsExpired { get; set; }

    }

   
}
