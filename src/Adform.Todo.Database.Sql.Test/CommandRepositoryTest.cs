using Adform.Todo.Database.Sql.Test.MockModel;
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
		private readonly DatabaseConnectionTest databaseConnection;
		private readonly List<DatabaseConnectionTest> databaseConnections;

		[Fact]
		public void Should_Add_Object_into_Database()
		{
			//Arrange
			var entityMock = databaseConnections.AsQueryable().BuildMockDbSet();
			var dbContext = new Mock<DbContext>();
			dbContext.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

			var commandRepository = CommandRepository<DatabaseConnectionTest>.GetInstance;
			commandRepository.Entities = entityMock.Object;
			commandRepository.TodoDatabase = dbContext.Object;

			//Act
			var result = commandRepository.Add(databaseConnection);

			//Assert
			result.Result.Should().BeGreaterThan(0);
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
			var commandRepository = CommandRepository<DatabaseConnectionTest>.GetInstance;

			entityMock.Setup(x => x.Update(entity));
			commandRepository.Entities = entityMock.Object;


			var dbContext = new Mock<DbContext>();
			dbContext.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

			commandRepository.Entities = entityMock.Object;
			commandRepository.TodoDatabase = dbContext.Object;

			return commandRepository;
		}

		[Fact]
		public void Should_Update_Object_into_Database()
		{
			//Arrange
			var entity = new Fixture().Create<DatabaseConnectionTest>();
			var commandRepository = ArrangeRepositoryForUpdate();

			//Act
			var result = commandRepository.Update(entity);

			//Assert
			result.Result.Should().BeGreaterThan(0);
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
			var commandRepository = CommandRepository<DatabaseConnectionTest>.GetInstance;

			entityMock.Setup(x => x.Remove(entity));
			commandRepository.Entities = entityMock.Object;


			var dbContext = new Mock<DbContext>();
			dbContext.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

			commandRepository.Entities = entityMock.Object;
			commandRepository.TodoDatabase = dbContext.Object;

			return commandRepository;
		}

		[Fact]
		public void Should_Remove_Object_from_Database()
		{
			//Arrange
			var entity = new Fixture().Create<DatabaseConnectionTest>();
			var commandRepository = ArrangeRepositoryForRemove();

			//Act
			var result = commandRepository.Remove(entity);

			//Assert
			result.Result.Should().BeGreaterThan(0);
		}
	}
}
