using Adform.Todo.Model.Entity;
using Microsoft.EntityFrameworkCore;
using System;

namespace Adform.Todo.Database.Sql.DataBaseContext
{
    /// <summary>
    /// To create the seed of default value into the database
    /// </summary>
    public class SeedTodoDatabase
    {
        public SeedTodoDatabase(ModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
        }
        private readonly ModelBuilder _modelBuilder;
        /// <summary>
        /// Seed user table
        /// </summary>
        public void SeedUser()
        {
            _modelBuilder.Entity<User>()
                   .ToTable("Users").HasData(new User()
                   {
                       Id = 1,
                       Name = "default",
                       Password = "AKDS+52ehoM="
                   });
            _modelBuilder.Entity<User>()
                    .ToTable("Users").HasData(new User()
                    {
                        Id = 2,
                        Name = "admin",
                        Password = "AKDS+52ehoM="
                    });
        }
        /// <summary>
        /// seed label table
        /// </summary>
        public void SeedLabel()
        {
            _modelBuilder.Entity<TodoLabel>()
                   .ToTable("Labels").HasData(new TodoLabel()
                   {
                       Id = 1,
                       Name = "None",
                   });
        }


        public void SeedTodoList()
        {
            _modelBuilder.Entity<TodoList>()
                   .ToTable("ToDoLists").HasData(new TodoList()
                   {
                       Id = 1,
                       Name = "None",
                       CreatedDate = DateTime.Now,
                       UpdatedDate = DateTime.Now,
                       LabelId = 1,
                       UserId = 1,
                   });
        }

        /// <summary>
        /// seed todoitem table
        /// </summary>
        public void SeedTodoItem()
        {
            _modelBuilder.Entity<TodoItem>()
                   .ToTable("ToDoItems").HasData(new TodoItem()
                   {
                       Id = 1,
                       Name = "None",
                       LabelId=1,
                       ToDoListId=1,
                       UserId=1
                   });
        }
    }
}
