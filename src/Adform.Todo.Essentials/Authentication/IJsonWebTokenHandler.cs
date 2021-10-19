using System;
using System.Collections.Generic;
using System.Text;

namespace Adform.Todo.Essentials.Authentication
{
    public interface IJsonWebTokenHandler
    {
        string GenerateJSONWebToken(string UserId);
        int? GetUserIdfromToken(string token);
    }
}
