﻿using Adform.Todo.Database.Sql.Test.MockModel;
using AutoFixture;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Xunit;

namespace Adform.Todo.Database.Sql.Test
{
    public class CommandRepositoryTest
    {
        public CommandRepositoryTest()
        {
			//Global Arrange
			databaseConnection = new Fixture().Create<DatabaseConnectionTest>();
			databaseConnections = new Fixture().Create<List<DatabaseConnectionTest>>();
		}
		DatabaseConnectionTest databaseConnection;
		List<DatabaseConnectionTest> databaseConnections;

		[Fact]
		public async void ShouldAddObjectIntoDatabase()
		{
			//Arrange
			var entityMock = databaseConnections.AsQueryable().BuildMockDbSet();
			var dbContext = new Mock<DbContext>();
			dbContext.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

			var commandRepository = new CommandRepository<DatabaseConnectionTest>(databaseConnection);
			commandRepository.Entities = entityMock.Object;
			commandRepository.TodoDatabase = dbContext.Object;

			//Act
			var result = await commandRepository.Add(databaseConnection);

			//Assert
			result.Should().BeGreaterThan(0);
		}

		/// <summary>
		/// This method is to create and mock 
		/// all required parameters to DbContext/Repository to perform Update
		/// </summary>
		/// <returns></returns>
		public CommandRepository<DatabaseConnectionTest> ArrangeRepositoryForUpdate()
		{
			var databseId = 1;
			var entity = new Fixture().Create<DatabaseConnectionTest>();
			entity.Id = databseId;
			var entityList = new Fixture().Create<List<DatabaseConnectionTest>>();
			entityList.Add(entity);

			var entityMock = entityList.AsQueryable().BuildMockDbSet();
			var commandRepository = new CommandRepository<DatabaseConnectionTest>(databaseConnection);

			entityMock.Setup(x => x.Update(entity));
			commandRepository.Entities = entityMock.Object;


			var dbContext = new Mock<DbContext>();
			dbContext.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

			commandRepository.Entities = entityMock.Object;
			commandRepository.TodoDatabase = dbContext.Object;

			return commandRepository;
		}

		[Fact]
		public async void ShouldUpdateObjectIntoDatabase()
		{
			//Arrange
			var entity = new Fixture().Create<DatabaseConnectionTest>();
			var commandRepository = ArrangeRepositoryForUpdate();

			//Act
			var result = await commandRepository.Update(entity);

			//Assert
			result.Should().BeGreaterThan(0);
		}

		/// <summary>
		/// This method is to create and mock 
		/// all required parameters to DbContext/Repository to perform remove
		/// </summary>
		/// <returns></returns>
		public CommandRepository<DatabaseConnectionTest> ArrangeRepositoryForRemove()
		{
			var databseId = 1;
			var entity = new Fixture().Create<DatabaseConnectionTest>();
			entity.Id = databseId;
			var entityList = new Fixture().Create<List<DatabaseConnectionTest>>();
			entityList.Add(entity);

			var entityMock = entityList.AsQueryable().BuildMockDbSet();
			var commandRepository = new CommandRepository<DatabaseConnectionTest>(databaseConnection);

			entityMock.Setup(x => x.Remove(entity));
			commandRepository.Entities = entityMock.Object;


			var dbContext = new Mock<DbContext>();
			dbContext.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

			commandRepository.Entities = entityMock.Object;
			commandRepository.TodoDatabase = dbContext.Object;

			return commandRepository;
		}

		[Fact]
		public async void ShouldRemoveObjectIntoDatabase()
		{
			//Arrange
			var entity = new Fixture().Create<DatabaseConnectionTest>();
			var commandRepository = ArrangeRepositoryForRemove();

			//Act
			var result = await commandRepository.Remove(entity);

			//Assert
			result.Should().BeGreaterThan(0);
		}
	}
}
