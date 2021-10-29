using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Adform.Todo.Model.Entity
{
    /// <summary>
    /// Class to hold information about todolist
    /// </summary>
    [Table("ToDoLists")]
    public class TodoList 
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }      
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        [ForeignKey("LabelId")]
        public int? LabelId { get; set; }

        [ForeignKey("UserId")]
        public int? UserId { get; set; }
        public virtual TodoLabel? Label { get; set; }
        public virtual User? Author { get; set; }
        public virtual List<TodoItem>? TodoItems { get; set; }
    }
}
