using Adform.Todo.DomainService;
using Adform.Todo.Model.Entity;
using AutoMapper;
using System.Linq;
using System.Threading.Tasks;

namespace Adform.Todo.Manager.Default
{
    /// <summary>
    /// To orchestrate the commands Action of label.
    /// Transform Dto to Entity
    /// </summary>
    public class LabelCommandManager : ILabelCommandManager
    {
        public LabelCommandManager(ILabelCommand labelCommand,
            ITodoItemCommandManager todoItemCommandManager,
            ITodoListCommandManager todoListCommandManager,
            ITodoItemQueryManager todoItemQueryManager,
            ITodoListQueryManager todoListQueryManager
            )
        {
            _labelCommand = labelCommand;
            _todoItemCommandManager = todoItemCommandManager;
            _todoListCommandManager = todoListCommandManager;
            _todoItemQueryManager = todoItemQueryManager;
            _todoListQueryManager = todoListQueryManager;
        }
        private readonly ILabelCommand _labelCommand;
        private readonly ITodoItemCommandManager _todoItemCommandManager;
        private readonly ITodoListCommandManager _todoListCommandManager;
        private readonly ITodoItemQueryManager _todoItemQueryManager;
        private readonly ITodoListQueryManager _todoListQueryManager;
        /// <summary>
        /// To add label into database.
        /// </summary>
        /// <param name="label">Single object of label.</param>
        /// <returns>Operation result</returns>
        public Task<int> Add(TodoLabel label)
        {
            return _labelCommand.Add(label);
        }
        /// <summary>
        /// To remove label into database.
        /// </summary>
        /// <param name="id">id of label to remove.</param>
        /// <returns>Operation result</returns>
        public async Task<int> DeletebyId(int id, int userId)
        {
            var items = (await _todoItemQueryManager.Get(userId)).Where(x => x.LabelId == id).ToList();
            await _todoItemCommandManager.DeleteRange(items);

            var lists = (await _todoListQueryManager.Get(userId)).Where(x => x.LabelId == id).ToList();
            await _todoListCommandManager.DeleteRange(lists);

            return await _labelCommand.DeletebyId(id);
        }

    }
}
