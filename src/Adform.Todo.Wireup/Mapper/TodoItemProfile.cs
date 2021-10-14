using Adform.Todo.Dto;
using Adform.Todo.Model.Entity;
using AutoMapper;

namespace Adform.Todo.Wireup.Mapper
{
    public class TodoItemProfile : Profile
    {
        public TodoItemProfile()
        {
            MapRequest();
            MapResponse();
        }
        private void MapRequest()
        {
            CreateMap<Item, ToDoItem>()
                .ForMember(to => to.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(to => to.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(to => to.Label, opt => opt.Ignore())
                .ForMember(to => to.LabelId, opt => opt.Ignore())
                .ForMember(to => to.ToDoItemList, opt => opt.Ignore())
                .ForMember(to => to.ToDoListId, opt => opt.Ignore())
                .ForMember(to => to.UserId, opt => opt.Ignore())
                .ForMember(to => to.Author, opt => opt.Ignore());
        }

        private void MapResponse()
        {
            CreateMap<ToDoItem, Item>()
                .ForMember(to => to.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(to => to.Name, opt => opt.MapFrom(src => src.Name))
                .ForPath(to => to.LabelName, opt => opt.MapFrom(src => src.Label.Name)).ReverseMap();
        }
    }
}
