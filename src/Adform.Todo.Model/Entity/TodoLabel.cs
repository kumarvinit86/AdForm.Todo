using System.ComponentModel.DataAnnotations.Schema;

namespace Adform.Todo.Model.Entity
{
    [Table("ToDoItems")]
    public class TodoLabel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual TodoItem? TodoItem { get; set; }
        public virtual TodoList? TodoList { get; set; }
    }
}
