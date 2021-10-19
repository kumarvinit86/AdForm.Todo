using Adform.Todo.Dto;
using Swashbuckle.AspNetCore.Filters;


namespace Adform.Todo.Api.SwaggerConfig
{
    public class AppUserExample : IExamplesProvider<AppUser>
    {
        public AppUser GetExamples()
        {
            return new AppUser { Name = "admin", Password = "admin" };
        }
    }
}
