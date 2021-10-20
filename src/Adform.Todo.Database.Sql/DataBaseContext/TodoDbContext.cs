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
        /// <param name="databaseConnection">Database connection instance to create the connection with database.</param>
        public TodoDbContext(DatabaseConnection databaseConnection)
            : base()
        {
            ConnectionString = databaseConnection.ToString();
            Database.EnsureCreated();
        }

        public string ConnectionString { get; set; }
        /// <summary>
        /// Override base OnConfigure method.
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString);
        }

        /// <summary>
        /// Override base OnModelCreating to define the entity relationship and relation between entity to database.
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ToDoItem>()
                    .ToTable("ToDoItems").HasOne(t => t.Author).WithOne(u => u.TodoItem);
            modelBuilder.Entity<ToDoItem>().HasIndex(x => x.UserId).IsUnique(false);
            modelBuilder.Entity<ToDoItem>().HasIndex(x => x.LabelId).IsUnique(false);
            modelBuilder.Entity<ToDoItem>().HasIndex(x => x.ToDoListId).IsUnique(false);
            modelBuilder.Entity<ToDoItem>()
                    .ToTable("ToDoItems").HasOne(t => t.ToDoItemList).WithMany(l => l.TodoItems);
            modelBuilder.Entity<ToDoItem>()
                    .ToTable("ToDoItems").HasOne(t => t.Label).WithOne(l => l.TodoItem).OnDelete(DeleteBehavior.ClientCascade);
            modelBuilder.Entity<User>()
                   .ToTable("Users");
            modelBuilder.Entity<TodoLabel>()
                  .ToTable("Labels");

            modelBuilder.Entity<ToDoList>()
                  .ToTable("ToDoLists").HasOne(t => t.Author).WithOne(u => u.TodoList);
            modelBuilder.Entity<ToDoList>()
                .ToTable("ToDoLists").HasOne(t => t.Label).WithOne(u => u.TodoList).OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<ToDoList>().HasIndex(x => x.UserId).IsUnique(false);
            modelBuilder.Entity<ToDoList>().HasIndex(x => x.LabelId).IsUnique(false);

            var seeds = new SeedTodoDatabase(modelBuilder);
            seeds.SeedUser();
            seeds.SeedLabel();
            seeds.SeedTodoList();
            seeds.SeedTodoItem();

        }
    }
}
