using System.Collections.Generic;

namespace Adform.Todo.Dto
{
    /// <summary>
    /// DTO for todolist
    /// </summary>
    public class ItemList : Item
    {
        public List<Item> ToDoItems { get; set; }
    }
}
