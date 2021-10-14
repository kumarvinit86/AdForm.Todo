using Adform.Todo.Model.Entity;
using Adform.Todo.Model.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;


namespace Adform.Todo.Service.Authentication
{
    /// <summary>
    /// To manage the jwt token related actions
    /// </summary>
    public class JsonWebTokenHandler
    {


        public JsonWebTokenHandler()
        {

        }
        public JsonWebTokenHandler(IConfiguration configuration)
        {
            SECRET_KEY = configuration.GetValue<string>("Secret_Key");
            ISSUER = configuration.GetValue<string>("Issuer");
            _configuration = configuration;
            SIGNING_KEY = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SECRET_KEY));
        }
        IConfiguration _configuration;
        private readonly string SECRET_KEY;
        private readonly string ISSUER;
        const int TIME_OUT = 30;
        public readonly SymmetricSecurityKey SIGNING_KEY;
        public string Server { get; set; }
        public string DataBase { get; set; }
        public string Token { get; set; }


        /// <summary>
        /// to generate the jwt toke
        /// </summary>
        /// <param name="UserId">User Id</param>
        /// <param name="isAuthenticated">Is user authenticated.</param>
        /// <returns></returns>
        public string GenerateJSONWebToken(string UserId, bool isAuthenticated)
        {
            var credentials = new SigningCredentials
                              (SIGNING_KEY, SecurityAlgorithms.HmacSha256Signature);

            var header = new JwtHeader(credentials);
            string username = UserId;
            var handler = new JwtSecurityTokenHandler();
            var authClaims = new List<Claim>
                {
                new Claim(ClaimTypes.Name, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

            var jwt = new JwtSecurityToken(
                                    issuer: _configuration.GetValue<string>("Issuer"),
                                    audience: _configuration.GetValue<string>("ValidAudience"),
                                    expires: DateTime.Now.AddHours(3),
                                    claims: authClaims,
                                    signingCredentials: new SigningCredentials(SIGNING_KEY, SecurityAlgorithms.HmacSha256)
                                    );

            // Token to String so you can use it in your client
            var tokenString = handler.WriteToken(jwt);
            return tokenString;
        }

        /// <summary>
        /// to refresh the jwt token in requests
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="isAuthenticated"></param>
        /// <returns></returns>
        public string RefreshJSONWebToken(string UserId, bool isAuthenticated)
        {
            string key = SECRET_KEY;
            var securityKey = new Microsoft
               .IdentityModel.Tokens.SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new Microsoft.IdentityModel.Tokens.SigningCredentials
                              (securityKey, SecurityAlgorithms.HmacSha256Signature);

            var header = new JwtHeader(credentials);
            string username = UserId;
            var handler = new JwtSecurityTokenHandler();
            var payload = new JwtPayload
            {
                {"user", username},
                {"issuer",ISSUER},
                {"notBefore", new DateTimeOffset(DateTime.Now).DateTime},
                {"expires", new DateTimeOffset(DateTime.Now.AddMinutes(TIME_OUT)).DateTime},
                {"isauth", isAuthenticated}
            };
            var jwt = new JwtSecurityToken(header, payload);
            // Token to String so you can use it in your client
            var tokenString = handler.WriteToken(jwt);
            return tokenString;
        }
        /// <summary>
        /// to decrypt the token one more time for wrapper security
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public string DecryptToken(string token)
        {
            string secret = SECRET_KEY;
            var key = Encoding.ASCII.GetBytes(secret);
            var handler = new JwtSecurityTokenHandler();
            var validations = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidateAudience = false
            };

            var token1 = handler.ReadJwtToken(token);
            var jwtOutput = string.Empty;
            var jwtHeader = JsonConvert.SerializeObject(token1.Header.Select(h => new { h.Value }));
            var jwtPayload = JsonConvert.SerializeObject(token1.Claims.Select(c => new { c.Value }));
            return jwtPayload;
        }

        /// <summary>
        /// to fetch the details of token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public JwtOptions FillJWTOption(string token)
        {

            string secret = SECRET_KEY;
            var key = Encoding.ASCII.GetBytes(secret);
            var handler = new JwtSecurityTokenHandler();
            var validations = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidateAudience = false
            };

            var jwToken = handler.ReadJwtToken(token);
            JwtOptions jwtOptions = new JwtOptions();
            jwtOptions.User = jwToken.Payload["user"].ToString();
            jwtOptions.Issuer = jwToken.Payload["issuer"].ToString();
            jwtOptions.ExpiryMinutes = Convert.ToDateTime(jwToken.Payload["expires"]);
            jwtOptions.NotBefore = DateTime.Now;
            jwtOptions.IsAuthenticated = Convert.ToBoolean(jwToken.Payload["isauth"]);
            return jwtOptions;

        }

        /// <summary>
        /// to fill the user info from the token
        /// </summary>
        /// <param name="token"></param>
        /// <returns>User object</returns>
        public Users FillUser(string token)
        {
            string secret = _configuration.GetValue<string>("Secret_Key");
            var key = Encoding.ASCII.GetBytes(secret);
            var handler = new JwtSecurityTokenHandler();
            var validations = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidateAudience = false
            };

            var jwToken = handler.ReadJwtToken(token);
            Users users = new Users();
            users.UserId = jwToken.Payload["user"].ToString();
            users.IsAuthenticated = Convert.ToBoolean(jwToken.Payload["isauth"]);
            return users;
        }

    }
}
