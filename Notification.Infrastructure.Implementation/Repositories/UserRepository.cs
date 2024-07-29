using Notification.Domain.Entities;
using Notification.Infrastructure.Implementation.Repositories.Abstractions;
using Notification.Service.Abstractions.Repositories.Abstractions;
using Notification.Service.Abstractions.Interfaces;

namespace Notification.Infrastructure.Implementation.Repositories
{
    public class UserRepository(IApplicationContext context) : BaseRepository<User>(context), IUserRepository
    {
    }
}
