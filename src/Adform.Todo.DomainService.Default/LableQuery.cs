using Adform.Todo.Database;
using Adform.Todo.Model.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Adform.Todo.DomainService.Default
{
    /// <summary>
    /// To operate the Query Action of Lable.
    /// </summary>
    public class LableQuery : ILableQuery
    {
        public LableQuery(IQueryRepository<TodoLable> queryRepository)
        {
            _queryRepository = queryRepository;
        }
        private readonly IQueryRepository<TodoLable> _queryRepository;
        /// <summary>
        /// Fetch all lables
        /// </summary>
        /// <returns>List of lables</returns>
        public async Task<List<TodoLable>> Get()
        {
            return await _queryRepository.FillEntities();
        }
        /// <summary>
        /// Fetch lable by the id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>Sinlge object of lable</returns>
        public async Task<TodoLable> GetbyId(int Id)
        {
            return await _queryRepository.FindById(Id);
        }
    }
}
