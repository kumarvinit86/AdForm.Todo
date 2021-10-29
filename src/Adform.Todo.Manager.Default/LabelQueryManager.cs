using Adform.Todo.DomainService;
using Adform.Todo.Model.Entity;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Adform.Todo.Manager.Default
{
    /// <summary>
    /// To orchestrate the Query Action of label.
    /// Transform Entity to Dto.
    /// </summary>
    public class LabelQueryManager : ILabelQueryManager
    {
        public LabelQueryManager(ILabelQuery labelQuery)
        {
            _labelQuery = labelQuery;
        }
        private readonly ILabelQuery _labelQuery;
        /// <summary>
        /// Fetch all labels
        /// </summary>
        /// <returns>List of labels</returns>
        public async Task<List<TodoLabel>> Get()
        {
            return  await _labelQuery.Get();
        }

        /// <summary>
        /// Fetch label by the id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>Single object of label</returns>
        public async Task<TodoLabel> GetbyId(int Id)
        {
            return await _labelQuery.GetbyId(Id);
        }
    }
}
