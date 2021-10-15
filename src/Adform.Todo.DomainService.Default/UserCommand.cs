using Adform.Todo.Database;
using Adform.Todo.Model.Entity;
using System.Threading.Tasks;

namespace Adform.Todo.DomainService.Default
{
    public class UserCommand : IUserCommand
    {
        public UserCommand(ICommandRepository<User> commandRepository)
        {
            _commandRepository = commandRepository;
        }

        private readonly ICommandRepository<User> _commandRepository;

        public async Task<int> Add(User user)
        {
            return await _commandRepository.Add(user);
        }
    }
}
