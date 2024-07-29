using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Notification.Domain.EntitiesDTO;
using Notification.Models;
using Notification.Service.UserService.Commands;
using Swashbuckle.AspNetCore.Annotations;

namespace Notification.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController(IMapper mapper, ISender sender) : ControllerBase
    {
        private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper), "Uninitialized property");
        private readonly ISender _sender = sender ?? throw new ArgumentNullException(nameof(sender), "Uninitialized property");

        /// <summary>
        /// Adds a new user.
        /// </summary>
        /// <param name="userModel">The user details to add.</param>
        /// <returns>The newly added user.</returns>
        [HttpPost]
        [SwaggerOperation(
            Summary = "ƒобавл€ет email пользовател€",
            Description = "ƒобавл€ет email пользовател€ в базу данных"
            )]
        [SwaggerResponse(StatusCodes.Status201Created, "ѕользователь добавлен в базу данных")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Ќе указано об€зательное поле или пользователь уже добавлен")]
        public async Task<ActionResult> AddUser([FromBody] UserModel userModel)
        {
            var userDto = _mapper.Map<UserDto>(userModel);
            await _sender.Send(new AddUserAsyncCommand(userDto));
            return Created();
        }
    }
}
