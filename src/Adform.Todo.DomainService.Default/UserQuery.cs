using Adform.Todo.Database;
using Adform.Todo.Model.Entity;
using System.Linq;
namespace Adform.Todo.DomainService.Default
{
    public class UserQuery : IUserQuery
    {
        public UserQuery(IQueryRepository<User> queryRepository)
        {
            _queryRepository = queryRepository;
        }
        private readonly IQueryRepository<User> _queryRepository;
        public User ValidateUser(User user)
        {
            return _queryRepository.Entities.Where(x => x.Name == user.Name && x.Password == user.Password).FirstOrDefault();          
        }
    }
}
