using Adform.Todo.Api.Controllers;
using Adform.Todo.Dto;
using Adform.Todo.Essentials.Authentication;
using Adform.Todo.Manager;
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
    public class TodoLabelTest
    {
        [Fact]
        public void Should_Fetch_all_Label()
        {
            //Arrange
            var labelQueryManager = new Mock<ILabelQueryManager>();
            var logger = new Mock<IDbLogger>();
            var shouldreturn = new Fixture().Create<Task<List<Label>>>();
            var jsonWebTokenHandler = new Mock<IJsonWebTokenHandler>();
            jsonWebTokenHandler.Setup(x => x.GetUserIdfromToken(It.IsAny<string>())).Returns(1);
            labelQueryManager.Setup(x => x.Get()).Returns(shouldreturn);
            var todoLabelController = new TodoLabelController(labelQueryManager.Object,
                null,
                logger.Object,
                jsonWebTokenHandler.Object);
            todoLabelController.ControllerContext.HttpContext = new DefaultHttpContext();
            todoLabelController.ControllerContext.HttpContext.Request.Headers["Authorization"] = "<token string>";
            //Act
            var result = (ObjectResult)todoLabelController.Get().Result;
            //Assert
            result.StatusCode.Should().Be(StatusCodes.Status200OK);
        }

        [Fact]
        public void Should_Fetch_Label_by_Id()
        {
            //Arrange
            var labelQueryManager = new Mock<ILabelQueryManager>();
            var logger = new Mock<IDbLogger>();
            var shouldreturn = new Fixture().Create<Task<Label>>();
            var jsonWebTokenHandler = new Mock<IJsonWebTokenHandler>();
            jsonWebTokenHandler.Setup(x => x.GetUserIdfromToken(It.IsAny<string>())).Returns(1);
            labelQueryManager.Setup(x => x.GetbyId(1)).Returns(shouldreturn);
            var todoLabelController = new TodoLabelController(labelQueryManager.Object,
                null,
                logger.Object, jsonWebTokenHandler.Object);
            todoLabelController.ControllerContext.HttpContext = new DefaultHttpContext();
            todoLabelController.ControllerContext.HttpContext.Request.Headers["Authorization"] = "<token string>";
            //Act
            var result = (ObjectResult)todoLabelController.GetbyId(1).Result;
            //Assert
            result.StatusCode.Should().Be(StatusCodes.Status200OK);
        }

        [Fact]
        public void Should_Add_Lable()
        {
            //Arrange
            var fixture = new Fixture();
            var labelCommandManager = new Mock<ILabelCommandManager>();
            var logger = new Mock<IDbLogger>();
            var shouldreturn = fixture.Create<Task<int>>();
            var addParameter = fixture.Create<Label>();
            var jsonWebTokenHandler = new Mock<IJsonWebTokenHandler>();
            jsonWebTokenHandler.Setup(x => x.GetUserIdfromToken(It.IsAny<string>())).Returns(1);
            labelCommandManager.Setup(x => x.Add(addParameter)).Returns(shouldreturn);
            var todoLabelController = new TodoLabelController(null,
                labelCommandManager.Object,
                logger.Object,
                jsonWebTokenHandler.Object);
            todoLabelController.ControllerContext.HttpContext = new DefaultHttpContext();
            todoLabelController.ControllerContext.HttpContext.Request.Headers["Authorization"] = "<token string>";
            //Act
            var result = (ObjectResult)todoLabelController.Post(addParameter).Result;
            //Assert
            result.StatusCode.Should().Be(StatusCodes.Status200OK);
        }

        [Fact]
        public void Should_Delete_Lable_by_Id()
        {
            //Arrange
            var fixture = new Fixture();
            var labelCommandManager = new Mock<ILabelCommandManager>();
            var logger = new Mock<IDbLogger>();
            var shouldreturn = fixture.Create<Task<int>>();
            var addParameter = fixture.Create<Label>();
            var jsonWebTokenHandler = new Mock<IJsonWebTokenHandler>();
            jsonWebTokenHandler.Setup(x => x.GetUserIdfromToken(It.IsAny<string>())).Returns(1);
            labelCommandManager.Setup(x => x.Add(addParameter)).Returns(shouldreturn);
            var todoLabelController = new TodoLabelController(null,
                labelCommandManager.Object,
                logger.Object,
                jsonWebTokenHandler.Object);
            todoLabelController.ControllerContext.HttpContext = new DefaultHttpContext();
            todoLabelController.ControllerContext.HttpContext.Request.Headers["Authorization"] = "<token string>";
            //Act
            var result = (ObjectResult)todoLabelController.Post(addParameter).Result;
            //Assert
            result.StatusCode.Should().Be(StatusCodes.Status200OK);
        }
    }
}
