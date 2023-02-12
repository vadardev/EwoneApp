using Ewone.Data.Repositories.Repository;
using Ewone.Data.Repositories.UnitToWork;
using Ewone.Data.Repositories.User;
using Ewone.Domain.Helpers.Jwt;

namespace Ewone.Api.Extensions;

public static class IoC
{
    public static void Resolve(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient(typeof(IRepository<>), typeof(Repository<>));
        serviceCollection.AddTransient<IUnitToWork, UnitToWork>();

        DataResolve(serviceCollection);
        DomainResolve(serviceCollection);
    }

    private static void DataResolve(IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<IUserRepository, UserRepository>();
    }

    private static void DomainResolve(IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<IJwtHelper, JwtHelper>();
    }
}