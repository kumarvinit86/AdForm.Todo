﻿using Adform.Todo.Api.Controllers;
using Adform.Todo.Dto;
using Adform.Todo.Manager;
using Adform.Todo.Model.Entity;
using Adform.Todo.Wireup.Authentication;
using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SeriLogger.DbLogger;
using System.Threading.Tasks;
using Xunit;

namespace Adform.Todo.Api.Test
{
    public class AuthTest
    {

        [Fact]
        public void Should_Generate_Token_for_Correct_Credential()
        {
            //Arrange
            var fixture = new Fixture();
            var userQueryManager = new Mock<IUserQueryManager>();
            var jsonWebTokenHandler = new Mock<IJsonWebTokenHandler>();
            var userParameter = fixture.Create<AppUser>();
            var shouldReturn = fixture.Build<User>().Without(x=>x.TodoItem).Create();
            userQueryManager.Setup(x => x.ValidateUser(userParameter)).Returns(shouldReturn);
            jsonWebTokenHandler.Setup(x => x.GenerateJSONWebToken(It.IsAny<string>())).Returns(string.Empty);

            var loginController = new AuthController(userQueryManager.Object, null, null, jsonWebTokenHandler.Object);
            //Act
            var result = (ObjectResult)loginController.Post(userParameter);
            //Assert
            result.StatusCode.Should().Be(StatusCodes.Status200OK);
        }

        [Fact]
        public void Should_Return_Unauthorise_Status_for_Wrong_Credential()
        {
            //Arrange
            var fixture = new Fixture();
            var userQueryManager = new Mock<IUserQueryManager>();
            var jsonWebTokenHandler = new Mock<IJsonWebTokenHandler>();
            var userParameter = fixture.Build<AppUser>().Without(x=>x.Password).Create();
            User user = null;
            userQueryManager.Setup(x => x.ValidateUser(userParameter)).Returns(user);
            jsonWebTokenHandler.Setup(x => x.GenerateJSONWebToken(It.IsAny<string>())).Returns(string.Empty);
            var loginController = new AuthController(userQueryManager.Object, null, null, jsonWebTokenHandler.Object);
            //Act
            var result = (UnauthorizedResult)loginController.Post(userParameter);
            //Assert
            result.StatusCode.Should().Be(StatusCodes.Status401Unauthorized);
        }

        [Fact]
        public void Should_Add_User()
        {
            //Arrange
            var fixture = new Fixture();
            var userCommandManager = new Mock<IUserCommandManager>();
            var logger = new Mock<IDbLogger>();
            var jsonWebTokenHandler = new Mock<IJsonWebTokenHandler>();
            var userParameter = fixture.Create<AppUser>();
            var shouldReturn = Task.Run(() => { return 1; });
            userCommandManager.Setup(x => x.Add(userParameter)).Returns(shouldReturn);

            var loginController = new AuthController(null, userCommandManager.Object, logger.Object,null);
            //Act
            var result = (ObjectResult)loginController.PostRegisterUser(userParameter).Result;
            //Assert
            result.StatusCode.Should().Be(StatusCodes.Status200OK);
        }


    }
}