using Adform.Todo.DomainService;
using Adform.Todo.Dto;
using Adform.Todo.Essentials;
using Adform.Todo.Model.Entity;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace Adform.Todo.Manager.Default
{
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
        public async Task<int> Add(AppUser appUser)
        {
            appUser.Password = new CryptoHandler(_configuration).Encrypt(appUser.Password);
            return await _userCommand.Add(_mapper.Map<User>(appUser));
        }
    }
}
