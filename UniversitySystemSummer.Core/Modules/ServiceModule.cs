using Autofac;
using UniversitySystemSummer.Core.Services;

namespace UniversitySystemSummer.Core.Modules
{
    public class ServiceModule : Module
    {
        
            protected override void Load(ContainerBuilder builder)
            {
                builder.RegisterAssemblyTypes(typeof(ServiceModule).Assembly)
                .Where(t => t.Name.EndsWith("Service"))
                .AsImplementedInterfaces()
                .PropertiesAutowired();
            }
        }
    }
