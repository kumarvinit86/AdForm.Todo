using System.ComponentModel.DataAnnotations.Schema;

namespace Adform.Todo.Model.Entity
{
    [Table("ToDoItems")]
    public class TodoLable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ToDoItem? TodoItem { get; set; }
    }
}
