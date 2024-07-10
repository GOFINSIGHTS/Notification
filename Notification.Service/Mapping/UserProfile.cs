using AutoMapper;
using Notification.Domain.Entities;
using Notification.Domain.EntitiesDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notification.Service.Mapping
{
    public sealed class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserDto, User>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember is not null));
            CreateMap<User, UserDto>();
        }
    }
}
