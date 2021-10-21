using Adform.Todo.Dto;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Adform.Todo.Wireup.Mapper
{
    public class ListItemProfile : Profile
    {
        public ListItemProfile()
        {
            CreateMap<ListItem, Item>()
               .ForMember(to => to.Id, opt => opt.MapFrom(src => src.Id))
               .ForMember(to => to.Name, opt => opt.MapFrom(src => src.Name))
               .ForMember(to => to.LabelName, opt => opt.MapFrom(src => src.LabelName));
        }
    }
}
