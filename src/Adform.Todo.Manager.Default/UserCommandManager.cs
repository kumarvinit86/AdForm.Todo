using Adform.Todo.DomainService;
using Adform.Todo.Essentials;
using Adform.Todo.Model.Entity;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace Adform.Todo.Manager.Default
{
    /// <summary>
    /// To orchestrate the commands Action of user.
    /// Transform Dto to Entity
    /// </summary>
    public class UserCommandManager : IUserCommandManager
    {
        public UserCommandManager(IUserCommand userCommand, IMapper mapper, IConfiguration configuration)
        {
            _userCommand = userCommand;
            _mapper = mapper;
            _configuration = configuration;
        }
        private readonly IUserCommand _userCommand;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        /// <summary>
        /// To register a new user
        /// </summary>
        /// <param name="appUser"></param>
        /// <returns></returns>
        public async Task<int> Add(User user)
        {
            user.Password = new CryptoHandler(_configuration).Encrypt(user.Password);
            return await _userCommand.Add(user);
        }
    }
}
