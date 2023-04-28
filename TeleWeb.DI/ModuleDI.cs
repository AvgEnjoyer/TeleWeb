using Autofac;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using TeleWeb.Application.Services.Interfaces;
using TeleWeb.Application.Services;
using TeleWeb.Data;


namespace TeleWeb.DI
{
    public class ModuleDI : Module
    {
        private readonly IConfiguration _configuration;

        public ModuleDI(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            RegisterDBContext(builder);
          //  RegisterIdentityDBContext(builder);
            RegisterRepositories(builder);
            RegisterServices(builder);
            RegisterConfiguration(builder);
            RegisterIdentity(builder);            
        }

       

        private void RegisterIdentity(ContainerBuilder builder)
        {
               // builder.RegisterType<UserManager<User>>().AsSelf();
                //builder.RegisterType<SignInManager<User>>().AsSelf();
            
        }

        private void RegisterConfiguration(ContainerBuilder builder)
        {
            builder.RegisterInstance(_configuration).As<IConfiguration>().SingleInstance();
        }

        private void RegisterDBContext(ContainerBuilder builder)
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            if (connectionString == null) throw new NullReferenceException("DefaultConnection isn't set");
            builder.RegisterType<TeleWebDbContext>()
                .WithParameter((pi, ctx) => pi.ParameterType == typeof(DbContextOptions<TeleWebDbContext>),
                      (pi, ctx) => DbContextOptionsFactory(connectionString))
                .SingleInstance();

            builder.Register(context => context.Resolve<TeleWebDbContext>().Database)
            .As<DatabaseFacade>()
            .SingleInstance();
        }
       
        private DbContextOptions<TeleWebDbContext> DbContextOptionsFactory(string connectionString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TeleWebDbContext>();
            optionsBuilder.UseSqlServer(connectionString);
            return optionsBuilder.Options;
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