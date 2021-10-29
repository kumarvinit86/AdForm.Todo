using AutoMapper;
using AutoMapper.Configuration;
using SimpleInjector;

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
