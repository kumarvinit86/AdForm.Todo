//using Adform.Todo.Dto;
//using Adform.Todo.Manager;
//using System.Threading.Tasks;
//namespace Adform.Todo.Api.GraphQl.Mutation
//{

//    /// <summary>
//    /// Class to hold mutations for GraphQL
//    /// </summary>
//    public class Mutation
//    {
//        private readonly ILabelCommandManager _labelCommandManager;
//        private readonly ITodoListCommandManager _todoListCommandManager;
//        private readonly ITodoItemCommandManager _todoItemCommandManager;


//        public Mutation(
//            ILabelCommandManager labelCommandManager,
//            ITodoListCommandManager todoListCommandManager,
//            ITodoItemCommandManager todoItemCommandManager
//          )
//        {
//            _labelCommandManager = labelCommandManager;
//            _todoListCommandManager = todoListCommandManager;
//            _todoItemCommandManager = todoItemCommandManager;

//        }

//        /// <summary>
//        /// Mutation to add label
//        /// </summary>
//        /// <param name="label"></param>
//        /// <returns></returns>
//        public async Task<int> AddLabel(Label label)
//        {
//            return await _labelCommandManager.Add(label);
//        }

//        /// <summary>
//        /// Mutation to delete label by id
//        /// </summary>
//        /// <param name="labelId"></param>
//        /// <returns></returns>
//        public async Task<int> DeleteLabelbyId(int id)
//        {
//            return await _labelCommandManager.DeletebyId(id);
//        }

//        /// <summary>
//        /// Mutation to assign label to item
//        /// </summary>
//        /// <param name="itemId"></param>
//        /// <param name="labelId"></param>
//        /// <returns></returns>
//        public async Task<int> UpdateLabeltoItem(int itemId, int labelId)
//        {
//            return await _todoItemCommandManager.Updatelabel(itemId,labelId);
//        }

//        /// <summary>
//        /// Mutation to assign label to list
//        /// </summary>
//        /// <param name="itemListId"></param>
//        /// <param name="labelId"></param>
//        /// <returns></returns>
//        public async Task<int> UpdateLabeltoItemList(int itemListId, int labelId)
//        {
//            return await _todoListCommandManager.Updatelabel(itemListId, labelId);
//        }

//        /// <summary>
//        /// Mutation to add todoitem
//        /// </summary>
//        /// <param name="item"></param>
//        /// <returns></returns>
//        public async Task<int> AddToDoItem(Item item)
//        {
//            return await _todoItemCommandManager.Add(item);
//        }

//        /// <summary>
//        /// Mutation to delete todoitem
//        /// </summary>
//        /// <param name="item"></param>
//        /// <returns></returns>
//        public async Task<int> DeleteToDoItem(Item item)
//        {
//            return await _todoItemCommandManager.Delete(item);
//        }

//        /// <summary>
//        /// Mutation to delete todoitem by id
//        /// </summary>
//        /// <param name="item"></param>
//        /// <returns></returns>
//        public async Task<int> DeleteToDoItembyId(int id)
//        {
//            return await _todoItemCommandManager.DeletebyId(id);
//        }

//        /// <summary>
//        /// Mutation to update todoitem
//        /// </summary>
//        /// <param name="item"></param>
//        /// <returns></returns>
//        public async Task<int> UpdateToDoItem(Item item)
//        {
//            return await _todoItemCommandManager.Update(item);
//        }

//        /// <summary>
//        /// Mutation to add todolist
//        /// </summary>
//        /// <param name="itemList"></param>
//        /// <returns></returns>
//        public async Task<int> AddToDoList(ItemList itemList)
//        {
//            return await _todoListCommandManager.Add(itemList);
//        }

//        /// <summary>
//        /// Mutation to delete todolist
//        /// </summary>
//        /// <param name="itemList"></param>
//        /// <returns></returns>
//        public async Task<int> DeleteToDoList(ItemList itemList)
//        {
//            return await _todoListCommandManager.Delete(itemList);
//        }

//        /// <summary>
//        /// Mutation to delete todolist by id
//        /// </summary>
//        /// <param name="item"></param>
//        /// <returns></returns>
//        public async Task<int> DeleteToDoListbyId(int id)
//        {
//            return await _todoListCommandManager.DeletebyId(id);
//        }

//        /// <summary>
//        /// Mutation to update todolist
//        /// </summary>
//        /// <param name="itemList"></param>
//        /// <returns></returns>
//        public async Task<int> UpdateToDoList(ItemList itemList)
//        {
//            return await _todoListCommandManager.Update(itemList);
//        }
//    }

//}
