using Adform.Todo.DomainService;
using Adform.Todo.Dto;
using Adform.Todo.Model.Entity;
using AutoMapper;
using System.Threading.Tasks;

namespace Adform.Todo.Manager.Default
{
    /// <summary>
    /// To orchestrate the commands Action of Lable.
    /// Tranform Dto to Entity
    /// </summary>
    public class LableCommandManager : ILableCommandManager
    {
        public LableCommandManager(ILableCommand lableCommand, IMapper mapper)
        {
            _lableCommand = lableCommand;
            _mapper = mapper;
        }
        private readonly ILableCommand _lableCommand;
        private readonly IMapper _mapper;
        /// <summary>
        /// To add Lable into database.
        /// </summary>
        /// <param name="lable">Single object of Lable.</param>
        /// <returns>Operation result</returns>
        public Task<int> Add(Lable lable)
        {
            return _lableCommand.Add(_mapper.Map<TodoLable>(lable));
        }
        /// <summary>
        /// To remove Lable into database.
        /// </summary>
        /// <param name="id">id of Lable to remove.</param>
        /// <returns>Operation result</returns>
        public Task<int> DeletebyId(int id)
        {
            return _lableCommand.DeletebyId(id);
        }

    }
}
