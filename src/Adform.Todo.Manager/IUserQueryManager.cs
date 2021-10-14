using Adform.Todo.Dto;
using Adform.Todo.Model.Entity;

namespace Adform.Todo.Manager
{
    public interface IUserQueryManager
    {
        User ValidateUser(AppUser user);
    }
}
