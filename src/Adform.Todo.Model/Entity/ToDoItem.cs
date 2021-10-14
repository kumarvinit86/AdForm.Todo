using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Adform.Todo.Model.Entity
{
    /// <summary>
    /// Class to information about todoitems
    /// </summary>
    [Table("ToDoItems")]
    public class ToDoItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        [ForeignKey("ToDoListId")]
        public int? ToDoListId { get; set; } = 1;

        [ForeignKey("LabelId")]
        public int? LabelId { get; set; } = 1;

        [ForeignKey("UserId")]
        public int? UserId { get; set; } = 2;
        public virtual ToDoList? ToDoItemList { get; set; }
        public virtual TodoLable? Label { get; set; }
        public virtual User? Author { get; set; }

    }
}
