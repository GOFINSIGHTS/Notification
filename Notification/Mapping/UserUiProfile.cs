using AutoMapper;
using Notification.Domain.EntitiesDTO;
using Notification.Models;

namespace Notification.Mapping
{
    internal sealed class UserUiProfile : Profile
    {
        public UserUiProfile()
        {
            CreateMap<UserDto, UserModel>();

            CreateMap<UserModel, UserDto>()
                .ForMember(x => x.Id, map => map.Ignore())
                .ForMember(x => x.Deleted, map => map.Ignore())
                .ForMember(x => x.Date, map => map.Ignore());            
        }
    }
}
