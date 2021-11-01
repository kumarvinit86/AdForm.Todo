using Adform.Todo.Wireup.Mapper;
using AutoMapper;
using AutoMapper.Configuration;


namespace Adform.Todo.Api.Test.Inititializer
{
	public static class MapperInitializer
	{
		public static IMapper Mapper { get; set; }
		static MapperInitializer()
		{
			var mapperConfigurationExpression = new MapperConfigurationExpression();
			mapperConfigurationExpression.AddProfile(new TodoItemProfile());
			mapperConfigurationExpression.AddProfile(new TodoLabelProfile());
			mapperConfigurationExpression.AddProfile(new TodoListProfile());
			mapperConfigurationExpression.AddProfile(new UserProfile());
			var mapperConfig = new MapperConfiguration(mapperConfigurationExpression);
			Mapper = mapperConfig.CreateMapper();
		}
	}
}
