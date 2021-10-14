using Adform.Todo.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Adform.Todo.Manager
{
    public interface ILableQueryManager
    {
        Task<List<Lable>> Get();
        Task<Lable> GetbyId(int Id);
    }
}
