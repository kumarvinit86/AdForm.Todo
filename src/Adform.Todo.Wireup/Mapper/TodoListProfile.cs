using Adform.Todo.Dto;
using Adform.Todo.Model.Entity;
using AutoMapper;

namespace Adform.Todo.Wireup.Mapper
{
    public class TodoListProfile : Profile
    {
        public TodoListProfile()
        {
            MapRequest();
            MapResponse();
        }
        private void MapRequest()
        {
            CreateMap<ItemList, TodoList>()
                .ForMember(to => to.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(to => to.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(to => to.TodoItems, opt => opt.MapFrom(src=>src.ChildItems))
                .ForPath(to => to.Label.Name, opt => opt.MapFrom(src => src.LabelName));
        }

        private void MapResponse()
        {
            CreateMap<TodoList, ItemList>()
                .ForMember(to => to.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(to => to.Name, opt => opt.MapFrom(src => src.Name))
                .ForPath(to => to.LabelName, opt => opt.MapFrom(src => src.Label.Name))
                .ForMember(to => to.ChildItems, opt => opt.MapFrom(src=>src.TodoItems));

        }
    }
}
