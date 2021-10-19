using Adform.Todo.Model.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Adform.Todo.Database.Sql.DataBaseContext
{
    public class SeedTodoDatabase
    {
        public SeedTodoDatabase(ModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
        }
        private readonly ModelBuilder _modelBuilder;
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
            _modelBuilder.Entity<ToDoList>()
                   .ToTable("ToDoLists").HasData(new ToDoList()
                   {
                       Id = 1,
                       Name = "None",
                       CreatedDate = DateTime.Now,
                       UpdatedDate = DateTime.Now,
                       LabelId = 1,
                       UserId = 1,
                   });
        }

        public void SeedTodoItem()
        {
            _modelBuilder.Entity<ToDoItem>()
                   .ToTable("ToDoItems").HasData(new ToDoItem()
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
