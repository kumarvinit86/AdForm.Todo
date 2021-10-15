using Adform.Todo.DomainService;
using Adform.Todo.Dto;
using Adform.Todo.Model.Entity;
using AutoMapper;
using System.Threading.Tasks;

namespace Adform.Todo.Manager.Default
{
    /// <summary>
    /// To orchestrate the commands Action of label.
    /// Tranform Dto to Entity
    /// </summary>
    public class LabelCommandManager : ILabelCommandManager
    {
        public LabelCommandManager(ILabelCommand labelCommand, IMapper mapper)
        {
            _labelCommand = labelCommand;
            _mapper = mapper;
        }
        private readonly ILabelCommand _labelCommand;
        private readonly IMapper _mapper;
        /// <summary>
        /// To add label into database.
        /// </summary>
        /// <param name="label">Single object of label.</param>
        /// <returns>Operation result</returns>
        public Task<int> Add(Label label)
        {
            return _labelCommand.Add(_mapper.Map<TodoLabel>(label));
        }
        /// <summary>
        /// To remove label into database.
        /// </summary>
        /// <param name="id">id of label to remove.</param>
        /// <returns>Operation result</returns>
        public Task<int> DeletebyId(int id)
        {
            return _labelCommand.DeletebyId(id);
        }

    }
}
