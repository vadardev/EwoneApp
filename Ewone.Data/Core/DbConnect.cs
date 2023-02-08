using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace Ewone.Data.Core;

public static class DbConnect
{
    public static void PgConnect(this IServiceCollection services)
    {
        string connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING") ?? throw new Exception("error");

        services.AddDbContext<EwoneDbContext>(o => o.UseNpgsql(connectionString));
    }
}