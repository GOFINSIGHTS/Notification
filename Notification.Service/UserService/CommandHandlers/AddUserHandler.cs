using AutoMapper;
using MediatR;
using Notification.Domain.Entities;
using Notification.Domain.EntitiesDTO;
using Notification.Service.Abstractions.Repositories.Interfaces;
using Notification.Service.UserService.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notification.Service.UserService.CommandHandlers
{
    public sealed class AddUserHandler(IUserRepository userRepository, IMapper mapper) : IRequestHandler<AddUserAsyncCommand, UserDto>
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<UserDto> Handle(AddUserAsyncCommand request, CancellationToken cancellationToken)
        {          
            request.User.Date = DateTime.UtcNow;
            _userRepository.Add(_mapper.Map<User>(request.User));

            await _userRepository.SaveChangesAsync(cancellationToken);

            return request.User;
        }
    }
}
