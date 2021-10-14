using Adform.Todo.Dto;
using Adform.Todo.Model.Entity;
using AutoMapper;

namespace Adform.Todo.Wireup.Mapper
{
    public class TodoLableProfile : Profile
    {
        public TodoLableProfile()
        {
            MapRequest();
            MapResponse();
        }
        private void MapRequest()
        {
            CreateMap<Lable, TodoLable>()
                .ForMember(to => to.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(to => to.Name, opt => opt.MapFrom(src => src.Name));
        }

        private void MapResponse()
        {
            CreateMap<TodoLable, Lable>()
                .ForMember(to => to.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(to => to.Name, opt => opt.MapFrom(src => src.Name));
        }
    }
}
