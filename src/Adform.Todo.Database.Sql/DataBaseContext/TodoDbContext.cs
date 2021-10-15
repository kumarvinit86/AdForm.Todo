using Adform.Todo.Model.Entity;
using Adform.Todo.Model.Models;
using Microsoft.EntityFrameworkCore;

namespace Adform.Todo.Database.Sql.DataBaseContext
{
    /// <summary>
    /// Custom Concrete database context for Todo.
    /// </summary>
    public class TodoDbContext : DbContext
    {
        /// <summary>
        /// Default Constructor of class
        /// </summary>
        /// <param name="databaseConnection">Database connection instanse to create the connection with database.</param>
        public TodoDbContext(DatabaseConnection databaseConnection)
            : base()
        {
            ConnectionString = databaseConnection.ToString();

        }

        public string ConnectionString { get; set; }
        /// <summary>
        /// Overrided base OnConfigure method.
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString);
        }

        /// <summary>
        /// Overrided base OnModelCreating to define the entity relationship and relation between entity to database.
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ToDoItem>()
                    .ToTable("ToDoItems").HasOne(t=>t.Author).WithOne(u=>u.TodoItem);
            modelBuilder.Entity<ToDoItem>()
                    .ToTable("ToDoItems").HasOne(t => t.ToDoItemList).WithMany(l=>l.TodoItems);
            modelBuilder.Entity<ToDoItem>()
                    .ToTable("ToDoItems").HasOne(t => t.Label).WithOne(l=>l.TodoItem);
            modelBuilder.Entity<User>()
                   .ToTable("Users");
            modelBuilder.Entity<TodoLabel>()
                  .ToTable("Labels");
            modelBuilder.Entity<ToDoList>()
                  .ToTable("ToDoLists");

        }
    }
}
