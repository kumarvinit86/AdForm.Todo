using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Adform.Todo.Model.Entity
{
    /// <summary>
    /// Class to information about todoitems
    /// </summary>
    [Table("ToDoItems")]
    public class TodoItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        [ForeignKey("ToDoListId")]
        public int? ToDoListId { get; set; }

        [ForeignKey("LabelId")]
        public int? LabelId { get; set; }

        [ForeignKey("UserId")]
        public int? UserId { get; set; }
        public virtual TodoList? ToDoItemList { get; set; }
        public virtual TodoLabel? Label { get; set; }
        public virtual User? Author { get; set; }

    }
}
