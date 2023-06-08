using AutoMapper;
using MIAUDataAgregation.Infrastructure.DataAccess;
using MIAUDataAgregation.Infrastructure.DataAccess.Repositories.Abstracts;
using MIAUDataAgregation.Infrastructure.DataAccess.Repositories.Implementation;
using MIAUDataAgregation.Infrastructure.Services.Abstracts;
using MIAUDataAgregation.Infrastructure.Services.Implementations;
using MIAUDataAgregation.Infrastructure.Services.Mappers;
using MIAUDataBase.Controllers.Mappers;
using Microsoft.EntityFrameworkCore;

namespace MIAUDataBase
{
    /// <summary>
    /// Класс для расширения builder.Services из Program.cs, работает с IServiceCollection
    /// </summary>
    public static class Registrar
    {
        /// <summary>
        /// Устанавливает мапперы, базу данных, репозитории и сервисы множеств
        /// (в смысле коллекции элементов дто, получаемых из бд)
        /// </summary>
        /// <param name="services"> Этот параметр получается из builder.Services </param>
        /// <param name="configuration"> Этот параметр можно получить из builder.Configuration </param>
        /// <returns></returns>
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .InstallMappers()
                .InstallDataBase(configuration)
                .InstallRepositories()
                .InstallSetServices();
            return services;
        }

        private static IServiceCollection InstallMappers(this IServiceCollection services)
        {
            var configuration = new MapperConfiguration(cfg => {
                cfg.AddProfile<ProfileLogDtoEntity>();
                cfg.AddProfile<ProfileServerPatientDtoEntity>();
                cfg.AddProfile<ProfileLogDtoModel>();
                cfg.AddProfile<ProfileServerPatientDtoModel>();
                cfg.AddProfile<ProfileLogModels>();
            });
            configuration.AssertConfigurationIsValid();
            services.AddSingleton<IMapper>(new Mapper(configuration));
            return services;
        }

        private static IServiceCollection InstallDataBase(
            this IServiceCollection services, IConfiguration configuration)
        {
            string? connection = configuration.GetConnectionString("Postgre");
            services
                .AddDbContext<ApplicationContext>(options => options.UseNpgsql(connection))
                .AddTransient<DbContext, ApplicationContext>();
            return services;
        }

        private static IServiceCollection InstallRepositories(this IServiceCollection services)
        {
            services
                .AddTransient<ILogRepository, LogRepository>()
                .AddTransient<IServerPatientRepository, ServerPatientRepository>();
            return services;
        }

        private static IServiceCollection InstallSetServices(this IServiceCollection services)
        {
            services
                .AddTransient<ILogSetService, LogSetService>()
                .AddTransient<IServerPatientSetService, ServerPatientSetService>();
            return services;
        }
    }
}
