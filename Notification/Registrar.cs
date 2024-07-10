using AutoMapper;
using Notification.Mapping;
using Notification.Service.Mapping;
using Notification.Infrastructure;
using Notification.Service.Abstractions.Repositories.Interfaces;
using Notification.Infrastructure.Implementation.Repositories;
using MediatR;
using Notification.Domain.EntitiesDTO;
using Notification.Service.UserService.CommandHandlers;
using Notification.Service.UserService.Commands;

namespace Notification
{
    internal static class Registrar
    {
        internal static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddSingleton((IConfigurationRoot)configuration)
                .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly))
                .AddSingleton<IMapper>(new Mapper(GetMapperConfiguration()))
                .AddInfrastructureServices(configuration)
                .InstallHandlers()
                .InstallRepositories();
        }

        private static MapperConfiguration GetMapperConfiguration()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<UserProfile>();
                cfg.AddProfile<UserUiProfile>();
            });
            configuration.AssertConfigurationIsValid();
            return configuration;
        }

        private static IServiceCollection InstallHandlers(this IServiceCollection serviceCollection)
        {
            serviceCollection
                //user
                .AddTransient<IRequestHandler<AddUserAsyncCommand, UserDto>, AddUserHandler>();
            return serviceCollection;
        }

        private static IServiceCollection InstallRepositories(this IServiceCollection serviceCollection)
        {
            serviceCollection
                .AddTransient<IUserRepository, UserRepository>();
            ;
            return serviceCollection;
        }
    }
}
