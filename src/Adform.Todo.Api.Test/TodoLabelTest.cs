using Adform.Todo.Api.Controllers;
using Adform.Todo.Dto;
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

            labelQueryManager.Setup(x => x.Get()).Returns(shouldreturn);
            var todoItemController = new TodoLabelController(labelQueryManager.Object,
                null,
                logger.Object);
            //Act
            var result = (ObjectResult)todoItemController.Get().Result;
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

            labelQueryManager.Setup(x => x.GetbyId(1)).Returns(shouldreturn);
            var todoItemController = new TodoLabelController(labelQueryManager.Object,
                null,
                logger.Object);
            //Act
            var result = (ObjectResult)todoItemController.GetbyId(1).Result;
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
            labelCommandManager.Setup(x => x.Add(addParameter)).Returns(shouldreturn);
            var todoLableController = new TodoLabelController(null,
                labelCommandManager.Object,
                logger.Object);
            //Act
            var result = (ObjectResult)todoLableController.Post(addParameter).Result;
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
            labelCommandManager.Setup(x => x.Add(addParameter)).Returns(shouldreturn);
            var todoLableController = new TodoLabelController(null,
                labelCommandManager.Object,
                logger.Object);
            //Act
            var result = (ObjectResult)todoLableController.Post(addParameter).Result;
            //Assert
            result.StatusCode.Should().Be(StatusCodes.Status200OK);
        }
    }
}
