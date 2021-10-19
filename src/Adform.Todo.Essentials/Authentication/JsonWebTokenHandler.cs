using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace Adform.Todo.Essentials.Authentication
{
    /// <summary>
    /// To manage the jwt token related actions
    /// </summary>
    public class JsonWebTokenHandler : IJsonWebTokenHandler
    {
        public JsonWebTokenHandler(IConfiguration configuration)
        {
            _configuration = configuration;
            SIGNING_KEY = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetValue<string>("Secret_Key")));
        }
        private readonly IConfiguration _configuration;
        const int TIME_OUT = 3;
        public readonly SymmetricSecurityKey SIGNING_KEY;

        /// <summary>
        /// to generate the jwt toke
        /// </summary>
        /// <param name="UserId">User Id</param>
        /// <returns></returns>
        public string GenerateJSONWebToken(string UserId)
        {
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
                                    expires: DateTime.Now.AddHours(TIME_OUT),
                                    claims: authClaims,
                                    signingCredentials: new SigningCredentials(SIGNING_KEY, SecurityAlgorithms.HmacSha256)
                                    );
            var tokenString = handler.WriteToken(jwt);
            return tokenString;
        }

        public int? GetUserIdfromToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwToken = handler.ReadJwtToken(token.Split(' ')[1]);
            int user;
            bool success = int.TryParse(jwToken.Payload["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"].ToString(), out user);
            if (success)
            {
                return user;
            }
            else
            {
                return null;
            }
        }


    }
}
