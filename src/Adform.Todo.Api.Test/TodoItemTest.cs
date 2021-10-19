using Adform.Todo.Api.Controllers;
using Adform.Todo.Dto;
using Adform.Todo.Essentials.Authentication;
using Adform.Todo.Manager;
using Adform.Todo.Model.Models;
using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SeriLogger.DbLogger;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Adform.Todo.Api.Test
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
            var jsonWebTokenHandler = new Mock<IJsonWebTokenHandler>();
            var pagingData = new Mock<PagingDataRequest>();
            var shouldreturn = new Fixture().Create<Task<ItemPaged>>();

            todoItemQueryManager.Setup(x=>x.Get(pagingData.Object,2)).Returns(shouldreturn);
            jsonWebTokenHandler.Setup(x => x.GetUserIdfromToken(It.IsAny<string>())).Returns(2);
            var todoItemController = new TodoItemController(todoItemQueryManager.Object, 
                todoItemCommandManager.Object,
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
        public void Should_Fetch_item_by_Id()
        {
            //Arrange
            var todoItemQueryManager = new Mock<ITodoItemQueryManager>();
            var todoItemCommandManager = new Mock<ITodoItemCommandManager>();
            var logger = new Mock<IDbLogger>();
            var jsonWebTokenHandler = new Mock<IJsonWebTokenHandler>();
            var shouldreturn = new Fixture().Create<Task<Item>>();
            todoItemQueryManager.Setup(x => x.GetbyId(2)).Returns(shouldreturn);

            var todoItemController = new TodoItemController(todoItemQueryManager.Object,
               todoItemCommandManager.Object,
               logger.Object,
               jsonWebTokenHandler.Object);
            //Act
            var result = (ObjectResult)todoItemController.GetbyId(2).Result;
            //Assert
            result.StatusCode.Should().Be(StatusCodes.Status200OK);
        }

        [Fact]
        public void Should_Add_Item_by_User()
        {
            //Arrange
            var fixture = new Fixture();
            var todoItemQueryManager = new Mock<ITodoItemQueryManager>();
            var todoItemCommandManager = new Mock<ITodoItemCommandManager>();
            var logger = new Mock<IDbLogger>();
            var jsonWebTokenHandler = new Mock<IJsonWebTokenHandler>();
            var shouldreturn = fixture.Create<Task<int>>();
            var addParameter = fixture.Create<Item>();          
            todoItemCommandManager.Setup(x => x.Add(addParameter)).Returns(shouldreturn);
            jsonWebTokenHandler.Setup(x => x.GetUserIdfromToken(It.IsAny<string>())).Returns(1);
            var todoItemController = new TodoItemController(todoItemQueryManager.Object,
              todoItemCommandManager.Object,
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
        public void Should_Update_Item()
        {
            //Arrange
            var todoItemQueryManager = new Mock<ITodoItemQueryManager>();
            var todoItemCommandManager = new Mock<ITodoItemCommandManager>();
            var logger = new Mock<IDbLogger>();
            var jsonWebTokenHandler = new Mock<IJsonWebTokenHandler>();
            var shouldreturn = new Fixture().Create<Task<int>>();
            var addParameter = new Fixture().Create<Item>();
            todoItemCommandManager.Setup(x => x.Update(addParameter)).Returns(shouldreturn);

            var todoItemController = new TodoItemController(todoItemQueryManager.Object,
              todoItemCommandManager.Object,
              logger.Object,
              jsonWebTokenHandler.Object);
            //Act
            var result = (ObjectResult)todoItemController.Patch(addParameter).Result;
            //Assert
            result.StatusCode.Should().Be(StatusCodes.Status200OK);
        }

        [Fact]
        public void Should_Update_Items_label()
        {
            //Arrange
            var todoItemQueryManager = new Mock<ITodoItemQueryManager>();
            var todoItemCommandManager = new Mock<ITodoItemCommandManager>();
            var logger = new Mock<IDbLogger>();
            var jsonWebTokenHandler = new Mock<IJsonWebTokenHandler>();
            var shouldreturn = new Fixture().Create<Task<int>>();           
            todoItemCommandManager.Setup(x => x.Updatelabel(1,1)).Returns(shouldreturn);

            var todoItemController = new TodoItemController(todoItemQueryManager.Object,
              todoItemCommandManager.Object,
              logger.Object,
              jsonWebTokenHandler.Object);
            //Act
            var result = (ObjectResult)todoItemController.PatchUpdatelabel(1,1).Result;
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
            var jsonWebTokenHandler = new Mock<IJsonWebTokenHandler>();
            var shouldreturn = new Fixture().Create<Task<int>>();
            todoItemCommandManager.Setup(x => x.DeletebyId(1)).Returns(shouldreturn);

            var todoItemController = new TodoItemController(todoItemQueryManager.Object,
              todoItemCommandManager.Object,
              logger.Object,
              jsonWebTokenHandler.Object);
            //Act
            var result = (ObjectResult)todoItemController.Delete(1).Result;
            //Assert
            result.StatusCode.Should().Be(StatusCodes.Status200OK);
        }
    }
}
