using Autofac;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TeleWeb.Data;
namespace TeleWeb.DI
{
    public class ModuleDI : Module
    {
        private IConfiguration _configuration;

        public ModuleDI(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            RegisterDBContext(builder);
            RegisterRepositories(builder);
            RegisterServices(builder);
        }

        private void RegisterDBContext(ContainerBuilder builder)
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");

            builder.RegisterType<TeleWebDbContext>()
                .WithParameter(new TypedParameter(typeof(DbContextOptions<DbContext>), connectionString))
                 .InstancePerLifetimeScope()
                 .OnActivated(x => x.Instance.Database.mig);
        }

        private void RegisterServices(ContainerBuilder builder)
        {
            var servicesAssembly = typeof(TeleWeb.Application.Services.Interfaces.IService).Assembly;
            builder.RegisterAssemblyTypes(servicesAssembly).
                Where(t => t.GetInterfaces()
                .Contains(typeof(TeleWeb.Application.Services.Interfaces.IService)))
                .AsImplementedInterfaces()
                .InstancePerDependency();
        }

        private void RegisterRepositories(ContainerBuilder builder)
        {
            var repositoriesAssembly = typeof(TeleWeb.Data.Repositories.Interfaces.IRepository).Assembly;
            builder.RegisterAssemblyTypes(repositoriesAssembly).
                Where(t => t.GetInterfaces()
                .Contains(typeof(TeleWeb.Data.Repositories.Interfaces.IRepository)))
                .AsImplementedInterfaces()
                .InstancePerDependency();
        }
    }
}