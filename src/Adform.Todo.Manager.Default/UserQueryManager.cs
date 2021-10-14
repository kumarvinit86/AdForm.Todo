using Adform.Todo.DomainService;
using Adform.Todo.Dto;
using Adform.Todo.Model.Entity;

namespace Adform.Todo.Manager.Default
{
    /// <summary>
    /// To orchestrate the Query Action of User.
    /// </summary>
    public class UserQueryManager : IUserQueryManager
    {
        public UserQueryManager(IUserQuery userQuery)
        {
            _userQuery = userQuery;
        }
        private readonly IUserQuery _userQuery;

        /// <summary>
        /// Fetch the valid user details
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public User ValidateUser(AppUser user)
        {
           return _userQuery.ValidateUser(new User() {Name=user.Name,Password=user.Password });
        }
    }
}
