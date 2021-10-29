
using Adform.Todo.Model.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Adform.Todo.Manager
{
    public interface ILabelQueryManager
    {
        Task<List<TodoLabel>> Get();
        Task<TodoLabel> GetbyId(int Id);
    }
}
