using Autofac;
namespace TeleWeb.DI
{
    public class ModuleDI : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            RegisterServices(builder);
            RegisterRepositories(builder);
        }

        private void RegisterServices(ContainerBuilder builder)
        {
            var servicesAssembly = typeof(TeleWeb.Application.Services.ServiceRunner).Assembly;
            builder.RegisterAssemblyTypes(servicesAssembly).
                Where(t => t.GetInterfaces()
                .Contains(typeof(TeleWeb.Application.Services.Interfaces.IService)))
                .AsImplementedInterfaces()
                .InstancePerDependency();
        }

        private void RegisterRepositories(ContainerBuilder builder)
        {
            var servicesAssembly = typeof(TeleWeb.Data.Repositories.Interfaces.IRepository).Assembly;
            builder.RegisterAssemblyTypes(servicesAssembly).
                Where(t => t.GetInterfaces()
                .Contains(typeof(TeleWeb.Data.Repositories.Interfaces.IRepository)))
                .AsImplementedInterfaces()
                .InstancePerDependency();
        }
    }
}