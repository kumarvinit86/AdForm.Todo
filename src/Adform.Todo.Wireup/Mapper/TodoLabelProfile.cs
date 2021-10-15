using Adform.Todo.Dto;
using Adform.Todo.Model.Entity;
using AutoMapper;

namespace Adform.Todo.Wireup.Mapper
{
    public class TodoLabelProfile : Profile
    {
        public TodoLabelProfile()
        {
            MapRequest();
            MapResponse();
        }
        private void MapRequest()
        {
            CreateMap<Label, TodoLabel>()
                .ForMember(to => to.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(to => to.Name, opt => opt.MapFrom(src => src.Name));
        }

        private void MapResponse()
        {
            CreateMap<TodoLabel, Label>()
                .ForMember(to => to.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(to => to.Name, opt => opt.MapFrom(src => src.Name));
        }
    }
}
