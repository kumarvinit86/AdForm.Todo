using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Adform.Todo.Model.Entity
{
    /// <summary>
    /// Class to manage user
    /// </summary>

    [Table("Users")]
    public class User 
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public virtual TodoItem? TodoItem { get; set; }
        public virtual TodoList? TodoList { get; set; }
    }
}
