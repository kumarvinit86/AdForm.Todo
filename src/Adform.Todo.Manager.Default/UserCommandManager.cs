using Adform.Todo.DomainService;
using Adform.Todo.Dto;
using Adform.Todo.Model.Entity;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Adform.Todo.Manager.Default
{
    public class UserCommandManager : IUserCommandManager
    {
        public UserCommandManager(IUserCommand userCommand, IMapper mapper)
        {
            _userCommand = userCommand;
            _mapper = mapper;
        }
        private readonly IUserCommand _userCommand;
        private readonly IMapper _mapper;
        public async Task<int> Add(AppUser appUser)
        {
            return await _userCommand.Add(_mapper.Map<User>(appUser));
        }
    }
}
