using Adform.Todo.DomainService;
using Adform.Todo.Essentials;
using Adform.Todo.Model.Entity;
using AutoMapper;
using Microsoft.Extensions.Configuration;

namespace Adform.Todo.Manager.Default
{
    /// <summary>
    /// To orchestrate the Query Action of User.
    /// </summary>
    public class UserQueryManager : IUserQueryManager
    {
        public UserQueryManager(IUserQuery userQuery, IConfiguration configuration)
        {
            _userQuery = userQuery;
            _configuration = configuration;
        }
        private readonly IUserQuery _userQuery;
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Fetch the valid user details
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public User ValidateUser(User user)
        {
            user.Password = new CryptoHandler(_configuration).Encrypt(user.Password);
            return _userQuery.ValidateUser(user);
        }
    }
}
