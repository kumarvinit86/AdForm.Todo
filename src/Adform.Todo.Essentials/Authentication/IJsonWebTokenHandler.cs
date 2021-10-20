namespace Adform.Todo.Essentials.Authentication
{
    public interface IJsonWebTokenHandler
    {
        string GenerateJSONWebToken(string UserId);
        int? GetUserIdfromToken(string token);
    }
}
