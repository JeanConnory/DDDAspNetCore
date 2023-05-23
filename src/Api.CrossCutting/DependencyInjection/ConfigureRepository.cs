using Api.Data.Context;
using Api.Data.Implementations;
using Api.Data.Repository;
using Api.Domain.Interfaces;
using Api.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Api.CrossCutting.DependencyInjection
{
    public class ConfigureRepository
    {
        public static void ConfigureDependenciesRepository(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            serviceCollection.AddScoped<IUserRepository, UserImplementation>();

            var connectioString = "Server=Localhost;Port=3306;Database=dbDddApi;Uid=root;Pwd=123456789";

            serviceCollection.AddDbContext<MyContext>(options =>
                options.UseMySql(
                    connectioString,
                    ServerVersion.AutoDetect(connectioString)));
        }
    }
}
