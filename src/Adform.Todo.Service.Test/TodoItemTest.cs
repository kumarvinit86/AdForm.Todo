using System;
using Xunit;
using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using Adform.Todo.Dto;
using Adform.Todo.Model.Models;
using Adform.Todo.Manager;
using SeriLogger.DbLogger;
using System.Threading.Tasks;
using Adform.Todo.Service;
using Adform.Todo.Service.Controllers;

namespace Adform.Todo.Service.Test
{
    public class TodoItemTest
    {
        [Fact]
        public void Should_Fetch_all_item_of_User()
        {
            //Arrange
            var todoItemQueryManager = new Mock<ITodoItemQueryManager>();
            var todoItemCommandManager = new Mock<ITodoItemCommandManager>();
            var logger = new Mock<IDbLogger>();
            var pagingData = new Mock<PagingDataRequest>();
            var shouldreturn = new Fixture().Create<Task<Tuple<List<Item>, PagingDataResponse>>>();
            todoItemQueryManager.Setup(x=>x.Get(pagingData.Object,2)).Returns(shouldreturn);

            var todoItemController = new TodoItemController(todoItemQueryManager.Object, todoItemCommandManager.Object,logger.Object);
            //Act
            var result = (ObjectResult)todoItemController.Get(pagingData.Object, 2).Result;
            //Assert
            result.StatusCode.Should().Be(StatusCodes.Status200OK);
        }

        [Fact]
        public void Should_Fetch_item_by_Id()
        {
            //Arrange
            var todoItemQueryManager = new Mock<ITodoItemQueryManager>();
            var todoItemCommandManager = new Mock<ITodoItemCommandManager>();
            var logger = new Mock<IDbLogger>();
            var pagingData = new Mock<PagingDataRequest>();
            var shouldreturn = new Fixture().Create<Task<Item>>();
            todoItemQueryManager.Setup(x => x.GetbyId(2)).Returns(shouldreturn);

            var todoItemController = new TodoItemController(todoItemQueryManager.Object, todoItemCommandManager.Object, logger.Object);
            //Act
            var result = (ObjectResult)todoItemController.GetbyId(2).Result;
            //Assert
            result.StatusCode.Should().Be(StatusCodes.Status200OK);
        }

        [Fact]
        public void Should_Add_Item_by_User()
        {
            //Arrange
            var todoItemQueryManager = new Mock<ITodoItemQueryManager>();
            var todoItemCommandManager = new Mock<ITodoItemCommandManager>();
            var logger = new Mock<IDbLogger>();
            var pagingData = new Mock<PagingDataRequest>();
            var shouldreturn = new Fixture().Create<Task<int>>();
            var addParameter = new Fixture().Create<Item>();
            todoItemCommandManager.Setup(x => x.Add(addParameter)).Returns(shouldreturn);

            var todoItemController = new TodoItemController(todoItemQueryManager.Object, todoItemCommandManager.Object, logger.Object);
            //Act
            var result = (ObjectResult)todoItemController.Post(addParameter).Result;
            //Assert
            result.StatusCode.Should().Be(StatusCodes.Status200OK);
        }

        [Fact]
        public void Should_Update_Item()
        {
            //Arrange
            var todoItemQueryManager = new Mock<ITodoItemQueryManager>();
            var todoItemCommandManager = new Mock<ITodoItemCommandManager>();
            var logger = new Mock<IDbLogger>();
            var pagingData = new Mock<PagingDataRequest>();
            var shouldreturn = new Fixture().Create<Task<int>>();
            var addParameter = new Fixture().Create<Item>();
            todoItemCommandManager.Setup(x => x.Update(addParameter)).Returns(shouldreturn);

            var todoItemController = new TodoItemController(todoItemQueryManager.Object, todoItemCommandManager.Object, logger.Object);
            //Act
            var result = (ObjectResult)todoItemController.Patch(addParameter).Result;
            //Assert
            result.StatusCode.Should().Be(StatusCodes.Status200OK);
        }

        [Fact]
        public void Should_Update_Items_Lable()
        {
            //Arrange
            var todoItemQueryManager = new Mock<ITodoItemQueryManager>();
            var todoItemCommandManager = new Mock<ITodoItemCommandManager>();
            var logger = new Mock<IDbLogger>();
            var pagingData = new Mock<PagingDataRequest>();
            var shouldreturn = new Fixture().Create<Task<int>>();           
            todoItemCommandManager.Setup(x => x.UpdateLable(1,1)).Returns(shouldreturn);

            var todoItemController = new TodoItemController(todoItemQueryManager.Object, todoItemCommandManager.Object, logger.Object);
            //Act
            var result = (ObjectResult)todoItemController.PatchUpdateLable(1,1).Result;
            //Assert
            result.StatusCode.Should().Be(StatusCodes.Status200OK);
        }

        [Fact]
        public void Should_Delete_Items_by_Id()
        {
            //Arrange
            var todoItemQueryManager = new Mock<ITodoItemQueryManager>();
            var todoItemCommandManager = new Mock<ITodoItemCommandManager>();
            var logger = new Mock<IDbLogger>();
            var pagingData = new Mock<PagingDataRequest>();
            var shouldreturn = new Fixture().Create<Task<int>>();
            todoItemCommandManager.Setup(x => x.DeletebyId(1)).Returns(shouldreturn);

            var todoItemController = new TodoItemController(todoItemQueryManager.Object, todoItemCommandManager.Object, logger.Object);
            //Act
            var result = (ObjectResult)todoItemController.Delete(1).Result;
            //Assert
            result.StatusCode.Should().Be(StatusCodes.Status200OK);
        }
    }
}
