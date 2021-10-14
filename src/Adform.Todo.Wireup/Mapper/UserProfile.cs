using Adform.Todo.Dto;
using Adform.Todo.Model.Entity;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Adform.Todo.Wireup.Mapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            MapRequest();
        }
        private void MapRequest()
        {
            CreateMap<AppUser, User>()
                .ForMember(to => to.Id, opt => opt.Ignore())
                .ForMember(to => to.TodoItem, opt => opt.Ignore())
                .ForMember(to => to.Password, opt => opt.MapFrom(src => src.Password))
                .ForMember(to => to.Name, opt => opt.MapFrom(src => src.Name));
        }

    }
}
