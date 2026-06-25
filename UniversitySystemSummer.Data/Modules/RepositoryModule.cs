using Autofac;
using UniversitySystemSummer.Data.Repositories;

namespace UniversitySystemSummer.Data.Modules
{
    public class RepositoryModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(RepositoryModule).Assembly)
                  .Where(t => t.Name.EndsWith("Repository"))
                  .AsImplementedInterfaces()
                  .PropertiesAutowired();
        }
    }
}
