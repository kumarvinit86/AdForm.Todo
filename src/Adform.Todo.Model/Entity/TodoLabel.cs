using System.ComponentModel.DataAnnotations.Schema;

namespace Adform.Todo.Model.Entity
{
    [Table("ToDoItems")]
    public class TodoLabel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ToDoItem? TodoItem { get; set; }
    }
}
