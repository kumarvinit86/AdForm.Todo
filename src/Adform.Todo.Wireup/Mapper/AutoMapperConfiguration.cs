using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.Configuration;
using AutoMapper;

namespace Adform.Todo.Wireup.Mapper
{
	public static class AutoMapperConfiguration
	{
		public static Container ConfigureAutomapper(this Container container)
		{
			var mapperConfigurationExpression = new MapperConfigurationExpression();
			mapperConfigurationExpression.AddProfile(new TodoItemProfile());
			mapperConfigurationExpression.AddProfile(new TodoLabelProfile());
			mapperConfigurationExpression.AddProfile(new TodoListProfile());
			mapperConfigurationExpression.AddProfile(new UserProfile());
			var mapperConfiguration = new MapperConfiguration(mapperConfigurationExpression);
			container.RegisterInstance(mapperConfiguration);
			container.RegisterInstance(mapperConfiguration.CreateMapper(container.GetInstance));
			return container;
		}
	}
}
