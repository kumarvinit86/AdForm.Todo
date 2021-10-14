using Adform.Todo.Model.Enum;
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

        /// <summary>
        /// Requires password to be encrypted to Base64 for registering new user
        /// </summary>
        public string Password { get; set; }
        public UserType UserType { get; set; }

        public virtual ToDoItem? TodoItem { get; set; }
    }
}
