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
    public class ToDoList 
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }      
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        [ForeignKey("LabelId")]
        public int? LabelId { get; set; } = 1;

        [ForeignKey("UserId")]
        public int? UserId { get; set; } = 1;
        public virtual List<ToDoItem>? TodoItems { get; set; }
        public virtual TodoLabel? Label { get; set; }
        public virtual User? Author { get; set; }
    }
}
