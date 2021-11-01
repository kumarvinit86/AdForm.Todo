using Adform.Todo.Api.Controllers;
using Adform.Todo.Api.Test.Inititializer;
using Adform.Todo.Dto;
using Adform.Todo.Essentials.Authentication;
using Adform.Todo.Manager;
using Adform.Todo.Model.Entity;
using Adform.Todo.Model.Models;
using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SeriLogger.DbLogger;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Adform.Todo.Api.Test
{
    public class TodoItemListTest
    {
        [Fact]
        public void Should_Fetch_all_ItemList_of_User()
        {
            //Arrange
            var todoListQueryManager = new Mock<ITodoListQueryManager>();
            var logger = new Mock<IDbLogger>();
            var jsonWebTokenHandler = new Mock<IJsonWebTokenHandler>();
            var pagingData = new Mock<PagingDataRequest>();
            var shouldreturn = new Fixture().Create<Task<List<TodoList>>>();
            todoListQueryManager.Setup(x => x.Get(pagingData.Object, 2)).Returns(shouldreturn);
            jsonWebTokenHandler.Setup(x => x.GetUserIdfromToken(It.IsAny<string>())).Returns(2);
            var todoItemController = new TodoListController(todoListQueryManager.Object,
                null,
                logger.Object,
                jsonWebTokenHandler.Object, MapperInitializer.Mapper);

            todoItemController.ControllerContext.HttpContext = new DefaultHttpContext();
            todoItemController.ControllerContext.HttpContext.Request.Headers["Authorization"] = "<token string>";
            //Act
            var result = (ObjectResult)todoItemController.Get(pagingData.Object).Result;
            //Assert
            result.StatusCode.Should().Be(StatusCodes.Status200OK);
        }

        [Fact]
        public void Should_Fetch_ItemList_by_Id()
        {
            //Arrange
            var todoListQueryManager = new Mock<ITodoListQueryManager>();
            var logger = new Mock<IDbLogger>();
            var jsonWebTokenHandler = new Mock<IJsonWebTokenHandler>();
            var shouldreturn = new Fixture().Create<Task<TodoList>>();
            todoListQueryManager.Setup(x => x.GetbyId(2,1)).Returns(shouldreturn);
            jsonWebTokenHandler.Setup(x => x.GetUserIdfromToken(It.IsAny<string>())).Returns(1);
            var todoItemController = new TodoListController(todoListQueryManager.Object,
                null,
                logger.Object,
                jsonWebTokenHandler.Object,MapperInitializer.Mapper);
            todoItemController.ControllerContext.HttpContext = new DefaultHttpContext();
            todoItemController.ControllerContext.HttpContext.Request.Headers["Authorization"] = "<token string>";
            //Act
            var result = (ObjectResult)todoItemController.GetbyId(2).Result;
            //Assert
            result.StatusCode.Should().Be(StatusCodes.Status200OK);
        }

        [Fact]
        public void Should_Add_ItemList_by_User()
        {
            //Arrange
            var fixture = new Fixture();
            var todoListCommandManager = new Mock<ITodoListCommandManager>();
            var logger = new Mock<IDbLogger>();
            var jsonWebTokenHandler = new Mock<IJsonWebTokenHandler>();
            var shouldreturn = fixture.Create<Task<int>>();
            var addParameter = fixture.Create<TodoList>();
            todoListCommandManager.Setup(x => x.Add(addParameter)).Returns(shouldreturn);
            jsonWebTokenHandler.Setup(x => x.GetUserIdfromToken(It.IsAny<string>())).Returns(1);
            var todoItemController = new TodoListController(null,
                        todoListCommandManager.Object,
                        logger.Object,
                        jsonWebTokenHandler.Object, MapperInitializer.Mapper);

            todoItemController.ControllerContext.HttpContext = new DefaultHttpContext();
            todoItemController.ControllerContext.HttpContext.Request.Headers["Authorization"] = "<token string>";
            //Act
            var result = (ObjectResult)todoItemController
                .Post(MapperInitializer.Mapper.Map<ItemList>(addParameter)).Result;
            //Assert
            result.StatusCode.Should().Be(StatusCodes.Status200OK);
        }

        [Fact]
        public void Should_Update_ItemList()
        {
            //Arrange
            var fixture = new Fixture();
            var todoListCommandManager = new Mock<ITodoListCommandManager>();
            var logger = new Mock<IDbLogger>();
            var jsonWebTokenHandler = new Mock<IJsonWebTokenHandler>();
            var shouldreturn = fixture.Create<Task<int>>();
            var addParameter = fixture.Create<TodoList>();
            todoListCommandManager.Setup(x => x.Update(addParameter)).Returns(shouldreturn);
            jsonWebTokenHandler.Setup(x => x.GetUserIdfromToken(It.IsAny<string>())).Returns(1);
            var todoItemController = new TodoListController(null,
                        todoListCommandManager.Object,
                        logger.Object,
                        jsonWebTokenHandler.Object,
                        MapperInitializer.Mapper);

            todoItemController.ControllerContext.HttpContext = new DefaultHttpContext();
            todoItemController.ControllerContext.HttpContext.Request.Headers["Authorization"] = "<token string>";
            //Act
            var result = (ObjectResult)todoItemController
                .Put(MapperInitializer.Mapper.Map<ItemList>(addParameter)).Result;
            //Assert
            result.StatusCode.Should().Be(StatusCodes.Status200OK);
        }

        [Fact]
        public void Should_Update_ItemList_Label()
        {
            //Arrange
            var fixture = new Fixture();
            var todoListCommandManager = new Mock<ITodoListCommandManager>();
            var logger = new Mock<IDbLogger>();
            var jsonWebTokenHandler = new Mock<IJsonWebTokenHandler>();
            var shouldreturn = fixture.Create<Task<int>>();
            todoListCommandManager.Setup(x => x.Updatelabel(1, 1,1)).Returns(shouldreturn);
            jsonWebTokenHandler.Setup(x => x.GetUserIdfromToken(It.IsAny<string>())).Returns(1);
            var todoItemController = new TodoListController(null,
                        todoListCommandManager.Object,
                        logger.Object,
                        jsonWebTokenHandler.Object,
                        MapperInitializer.Mapper);

            todoItemController.ControllerContext.HttpContext = new DefaultHttpContext();
            todoItemController.ControllerContext.HttpContext.Request.Headers["Authorization"] = "<token string>";
            //Act
            var result = (ObjectResult)todoItemController.PutUpdatelabel(1, 1).Result;
            //Assert
            result.StatusCode.Should().Be(StatusCodes.Status200OK);
        }

        [Fact]
        public void Should_Delete_ItemList_by_Id()
        {
            //Arrange
            var fixture = new Fixture();
            var todoListCommandManager = new Mock<ITodoListCommandManager>();
            var logger = new Mock<IDbLogger>();
            var jsonWebTokenHandler = new Mock<IJsonWebTokenHandler>();
            var shouldreturn = fixture.Create<Task<int>>();
            todoListCommandManager.Setup(x => x.DeletebyId(1,1)).Returns(shouldreturn);
            jsonWebTokenHandler.Setup(x => x.GetUserIdfromToken(It.IsAny<string>())).Returns(1);
            var todoItemController = new TodoListController(null,
                        todoListCommandManager.Object,
                        logger.Object,
                        jsonWebTokenHandler.Object,
                        MapperInitializer.Mapper);

            todoItemController.ControllerContext.HttpContext = new DefaultHttpContext();
            todoItemController.ControllerContext.HttpContext.Request.Headers["Authorization"] = "<token string>";
            //Act
            var result = (ObjectResult)todoItemController.Delete(1).Result;
            //Assert
            result.StatusCode.Should().Be(StatusCodes.Status200OK);
        }
    }
}
