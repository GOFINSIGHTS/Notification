using Notification.Domain.Entities;
using Notification.Service.Abstractions.Repositories.Abstractions.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notification.Service.Abstractions.Repositories.Abstractions
{
    public interface IUserRepository : IRepository<User>
    {
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
