using Adform.Todo.DomainService;
using Adform.Todo.Dto;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Adform.Todo.Manager.Default
{
    /// <summary>
    /// To orchestrate the Query Action of label.
    /// Tranform Entity to Dto.
    /// </summary>
    public class LabelQueryManager : ILabelQueryManager
    {
        public LabelQueryManager(ILabelQuery labelQuery, IMapper mapper)
        {
            _labelQuery = labelQuery;
            _mapper = mapper;
        }
        private readonly ILabelQuery _labelQuery;
        private readonly IMapper _mapper;
        /// <summary>
        /// Fetch all labels
        /// </summary>
        /// <returns>List of labels</returns>
        public async Task<List<Label>> Get()
        {
            return  _mapper.Map<List<Label>>(await _labelQuery.Get());
        }

        /// <summary>
        /// Fetch label by the id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>Sinlge object of label</returns>
        public async Task<Label> GetbyId(int Id)
        {
            return _mapper.Map<Label>(await _labelQuery.GetbyId(Id));
        }
    }
}
