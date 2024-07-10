using MediatR;
using Notification.Domain.EntitiesDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notification.Service.UserService.Commands
{
    public sealed record AddUserAsyncCommand(UserDto User) : IRequest<UserDto>;
}
