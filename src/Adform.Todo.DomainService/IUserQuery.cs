using Adform.Todo.Model.Entity;

namespace Adform.Todo.DomainService
{
    public interface IUserQuery
    {
        User ValidateUser(User user);
    }
}
