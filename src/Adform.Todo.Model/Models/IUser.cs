namespace Adform.Todo.Model.Models
{
    public interface IUser
    {
        string UserId { get; set; }
        string Password { get; set; }
        bool IsAuthenticated { get; set; }
        string JwtBearerToken { get; set; }
        bool IsExpired { get; set; }
    }
}
