using Notification.Domain.Entities;
using Notification.Service.Abstractions.Repositories.Interfaces.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notification.Service.Abstractions.Repositories.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
