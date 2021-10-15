using Adform.Todo.Database;
using Adform.Todo.Model.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Adform.Todo.DomainService.Default
{
    /// <summary>
    /// To operate the Query Action of label.
    /// </summary>
    public class LabelQuery : ILabelQuery
    {
        public LabelQuery(IQueryRepository<TodoLabel> queryRepository)
        {
            _queryRepository = queryRepository;
        }
        private readonly IQueryRepository<TodoLabel> _queryRepository;
        /// <summary>
        /// Fetch all labels
        /// </summary>
        /// <returns>List of labels</returns>
        public async Task<List<TodoLabel>> Get()
        {
            return await _queryRepository.FillEntities();
        }
        /// <summary>
        /// Fetch label by the id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>Sinlge object of label</returns>
        public async Task<TodoLabel> GetbyId(int Id)
        {
            return await _queryRepository.FindById(Id);
        }
    }
}
