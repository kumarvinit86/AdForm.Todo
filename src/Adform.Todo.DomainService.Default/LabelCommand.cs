using Adform.Todo.Database;
using Adform.Todo.Model.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Adform.Todo.DomainService.Default
{
    /// <summary>
    /// To operate the commands Action of label.
    /// </summary>
    public class LabelCommand : ILabelCommand
    {
        public LabelCommand(ICommandRepository<TodoLabel> commandRepository)
        {
            _commandRepository = commandRepository;
        }

        private readonly ICommandRepository<TodoLabel> _commandRepository;
        /// <summary>
        /// To add label into database.
        /// </summary>
        /// <param name="label">Single object of label.</param>
        /// <returns>Operation result</returns>
        public async Task<int> Add(TodoLabel label)
        {
            return await _commandRepository.Add(label);
        }
        /// <summary>
        /// To remove label into database.
        /// </summary>
        /// <param name="id">id of label to remove.</param>
        /// <returns>Operation result</returns>
        public async Task<int> DeletebyId(int id)
        {
            var data = _commandRepository.Entities.Where(x => x.Id == id).FirstOrDefault();
            if (data != null)
                return await _commandRepository.Remove(data);
            else { return 0; }
        }
     
    }
}
