using Adform.Todo.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Adform.Todo.Manager
{
    public interface ILabelQueryManager
    {
        Task<List<Label>> Get();
        Task<Label> GetbyId(int Id);
    }
}
