using Adform.Todo.Database;
using Adform.Todo.Model.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Adform.Todo.DomainService.Default
{
    /// <summary>
    /// To operate the commands Action of Lable.
    /// </summary>
    public class LableCommand : ILableCommand
    {
        public LableCommand(ICommandRepository<TodoLable> commandRepository)
        {
            _commandRepository = commandRepository;
        }

        private readonly ICommandRepository<TodoLable> _commandRepository;
        /// <summary>
        /// To add Lable into database.
        /// </summary>
        /// <param name="lable">Single object of Lable.</param>
        /// <returns>Operation result</returns>
        public async Task<int> Add(TodoLable lable)
        {
            return await _commandRepository.Add(lable);
        }
        /// <summary>
        /// To remove Lable into database.
        /// </summary>
        /// <param name="id">id of Lable to remove.</param>
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
