using Microsoft.EntityFrameworkCore;
using Notification.Domain.Entities;
using Notification.Service.Abstractions.Interfaces;
using System.Reflection;

namespace Notification.Infrastructure.PostgreSql.Persistance
{
    public sealed class NotificationContext(DbContextOptions<NotificationContext> options) : DbContext(options), IApplicationContext
    {
        public DbSet<User> Users { get; set; }               
    }
}
