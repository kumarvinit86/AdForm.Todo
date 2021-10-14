using System;

namespace Adform.Todo.Service.Authentication
{
    public class JwtOptions : IJwtOptions
    {
        public string SecretKey { get; set; }
        public DateTime ExpiryMinutes { get; set; }
        public string Issuer { get; set; }
        public DateTime NotBefore { get; set; }
        public bool IsAuthenticated { get; set; }
        public string User { get; set; }
    }


    public interface IJwtOptions
    {
        string SecretKey { get; set; }
        DateTime ExpiryMinutes { get; set; }
        string Issuer { get; set; }
        DateTime NotBefore { get; set; }
        bool IsAuthenticated { get; set; }
        string User { get; set; }
    }
}