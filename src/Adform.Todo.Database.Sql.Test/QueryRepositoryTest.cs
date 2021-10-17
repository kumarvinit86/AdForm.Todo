using Adform.Todo.Database.Sql.Test.MockModel;
using Adform.Todo.Model.Models;
using AutoFixture;
using FluentAssertions;
using MockQueryable.Moq;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Adform.Todo.Database.Sql.Test
{
    public class QueryRepositoryTest
    {
        private readonly DatabaseConnection databaseConnection;
        public QueryRepositoryTest()
        {
            //Global Arrange
            databaseConnection = new Fixture().Create<DatabaseConnection>();
        }

		[Fact]
		public void RepositoryShouldReturnEntities()
		{
			//Arrange
			var databaseConnections = new Fixture().Create<List<DatabaseConnection>>();
			var entityMock = databaseConnections.AsQueryable().BuildMockDbSet();
			var queryRepository = new QueryRepository<DatabaseConnection>(databaseConnection);
			queryRepository.Entities = entityMock.Object;

			//Act
			var result = queryRepository.FillEntities();

			//Assert
			result.Result.Should().BeOfType<List<DatabaseConnection>>();
		}

		[Fact]
		public void RepositoryShouldReturnEntitiesById()
		{
			//Arrange
			var databseId = 1;
			var entity = new Fixture().Create<DatabaseConnectionTest>();
			entity.Id = databseId;
			var entityList = new Fixture().Create<List<DatabaseConnectionTest>>();
			entityList.Add(entity);

			var entityMock = entityList.AsQueryable().BuildMockDbSet();
			var queryRepository = new QueryRepository<DatabaseConnectionTest>(databaseConnection);
			entityMock.Setup(x => x.FindAsync(databseId)).ReturnsAsync((object[] ids) =>
			{
				return entityList.FirstOrDefault(x => x.Id == databseId);
			});
			queryRepository.Entities = entityMock.Object;

			//Act
			var result = queryRepository.FindById(databseId);

			//Assert
			result.Result.Should().BeOfType<DatabaseConnectionTest>();
		}


	}
}
