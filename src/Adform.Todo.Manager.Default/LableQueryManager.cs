using Adform.Todo.DomainService;
using Adform.Todo.Dto;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Adform.Todo.Manager.Default
{
    /// <summary>
    /// To orchestrate the Query Action of Lable.
    /// Tranform Entity to Dto.
    /// </summary>
    public class LableQueryManager : ILableQueryManager
    {
        public LableQueryManager(ILableQuery lableQuery, IMapper mapper)
        {
            _lableQuery = lableQuery;
            _mapper = mapper;
        }
        private readonly ILableQuery _lableQuery;
        private readonly IMapper _mapper;
        /// <summary>
        /// Fetch all lables
        /// </summary>
        /// <returns>List of lables</returns>
        public async Task<List<Lable>> Get()
        {
            return  _mapper.Map<List<Lable>>(await _lableQuery.Get());
        }

        /// <summary>
        /// Fetch lable by the id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>Sinlge object of lable</returns>
        public async Task<Lable> GetbyId(int Id)
        {
            return _mapper.Map<Lable>(await _lableQuery.GetbyId(Id));
        }
    }
}
