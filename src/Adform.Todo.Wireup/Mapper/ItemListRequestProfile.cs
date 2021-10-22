using Adform.Todo.Dto;
using Adform.Todo.Model.Entity;
using AutoMapper;

namespace Adform.Todo.Wireup.Mapper
{
    public class ItemListRequestProfile : Profile
    {
        public ItemListRequestProfile()
        {
            MapRequest();
            MapResponse();
        }

        private void MapRequest()
        {
            CreateMap<ItemListRequest, ToDoList>()
                .ForMember(to => to.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(to => to.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(to => to.TodoItems, opt => opt.Ignore())
                .ForPath(to => to.Label.Name, opt => opt.MapFrom(src => src.LabelName))
                .ReverseMap();
        }

        private void MapResponse()
        {
            CreateMap<ItemList, ItemListRequest>()
                .ForMember(to => to.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(to => to.Name, opt => opt.MapFrom(src => src.Name))
                .ForPath(to => to.LabelName, opt => opt.MapFrom(src => src.LabelName)).ReverseMap();
        }

    }
}
