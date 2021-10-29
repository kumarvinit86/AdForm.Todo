using Adform.Todo.Model.Entity;

namespace Adform.Todo.Manager
{
    public interface IUserQueryManager
    {
        User ValidateUser(User user);
    }
}
