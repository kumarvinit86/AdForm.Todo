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
            CreateMap<ItemList, ToDoList>()
                .ForMember(to => to.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(to => to.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(to => to.TodoItems, opt => opt.MapFrom(src => src.ToDoItems))
                .ForPath(to => to.Label.Name, opt => opt.MapFrom(src => src.LabelName)).ReverseMap();
        }

        private void MapResponse()
        {
            CreateMap<ToDoList, ItemList>()
                .ForMember(to => to.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(to => to.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(to => to.ToDoItems, opt => opt.MapFrom(src => src.TodoItems))
                .ForPath(to => to.LabelName, opt => opt.MapFrom(src => src.Label.Name)).ReverseMap();
        }
    }
}
