using Adform.Todo.Api.Controllers;
using Adform.Todo.Dto;
using Adform.Todo.Essentials.Authentication;
using Adform.Todo.Manager;
using Adform.Todo.Model.Models;
using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SeriLogger.DbLogger;
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
            var shouldreturn = new Fixture().Create<Task<ItemListPaged>>();

            todoListQueryManager.Setup(x => x.Get(pagingData.Object, 2)).Returns(shouldreturn);
            jsonWebTokenHandler.Setup(x => x.GetUserIdfromToken(It.IsAny<string>())).Returns(2);
            var todoItemController = new TodoListController(todoListQueryManager.Object,
                null,
                logger.Object,
                jsonWebTokenHandler.Object);

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
            var shouldreturn = new Fixture().Create<Task<ItemList>>();
            todoListQueryManager.Setup(x => x.GetbyId(2)).Returns(shouldreturn);

            var todoItemController = new TodoListController(todoListQueryManager.Object,
                null,
                logger.Object,
                jsonWebTokenHandler.Object);
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
            var addParameter = fixture.Create<ItemList>();
            todoListCommandManager.Setup(x => x.Add(addParameter)).Returns(shouldreturn);
            jsonWebTokenHandler.Setup(x => x.GetUserIdfromToken(It.IsAny<string>())).Returns(1);
            var todoItemController = new TodoListController(null,
                        todoListCommandManager.Object,
                        logger.Object,
                        jsonWebTokenHandler.Object);

            todoItemController.ControllerContext.HttpContext = new DefaultHttpContext();
            todoItemController.ControllerContext.HttpContext.Request.Headers["Authorization"] = "<token string>";
            //Act
            var result = (ObjectResult)todoItemController.Post(addParameter).Result;
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
            var addParameter = fixture.Create<ItemList>();
            todoListCommandManager.Setup(x => x.Update(addParameter)).Returns(shouldreturn);
            jsonWebTokenHandler.Setup(x => x.GetUserIdfromToken(It.IsAny<string>())).Returns(1);
            var todoItemController = new TodoListController(null,
                        todoListCommandManager.Object,
                        logger.Object,
                        jsonWebTokenHandler.Object);

            todoItemController.ControllerContext.HttpContext = new DefaultHttpContext();
            todoItemController.ControllerContext.HttpContext.Request.Headers["Authorization"] = "<token string>";
            //Act
            var result = (ObjectResult)todoItemController.Put(addParameter).Result;
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
            todoListCommandManager.Setup(x => x.Updatelabel(1,1)).Returns(shouldreturn);
            jsonWebTokenHandler.Setup(x => x.GetUserIdfromToken(It.IsAny<string>())).Returns(1);
            var todoItemController = new TodoListController(null,
                        todoListCommandManager.Object,
                        logger.Object,
                        jsonWebTokenHandler.Object);

            todoItemController.ControllerContext.HttpContext = new DefaultHttpContext();
            todoItemController.ControllerContext.HttpContext.Request.Headers["Authorization"] = "<token string>";
            //Act
            var result = (ObjectResult)todoItemController.PutUpdatelabel(1,1).Result;
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
            todoListCommandManager.Setup(x => x.DeletebyId(1)).Returns(shouldreturn);
            jsonWebTokenHandler.Setup(x => x.GetUserIdfromToken(It.IsAny<string>())).Returns(1);
            var todoItemController = new TodoListController(null,
                        todoListCommandManager.Object,
                        logger.Object,
                        jsonWebTokenHandler.Object);

            todoItemController.ControllerContext.HttpContext = new DefaultHttpContext();
            todoItemController.ControllerContext.HttpContext.Request.Headers["Authorization"] = "<token string>";
            //Act
            var result = (ObjectResult)todoItemController.Delete(1).Result;
            //Assert
            result.StatusCode.Should().Be(StatusCodes.Status200OK);
        }
    }
}
