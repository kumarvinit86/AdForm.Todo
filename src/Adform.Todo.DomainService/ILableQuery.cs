using Adform.Todo.Model.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Adform.Todo.DomainService
{
    public interface ILableQuery
    {
        Task<List<TodoLable>> Get();
        Task<TodoLable> GetbyId(int Id);
    }
}
