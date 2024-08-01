using Autofac;
using MenuMasterAPI.Application.Interfaces;
using MenuMasterAPI.Application.Mappers;
using MenuMasterAPI.Application.Services;
using MenuMasterAPI.Domain.Repositories;
using MenuMasterAPI.Infrastructure.Data.Context;
using MenuMasterAPI.Infrastructure.Repositories;
using System.Reflection;
using Module = Autofac.Module;

namespace MenuMasterAPI.WebAPI.AutoFac;

public class AutoFacModule : Module
{
    protected override void Load(ContainerBuilder containerBuilder)
    {
        containerBuilder.RegisterGeneric(typeof(GenericRepository<,>)).As(typeof(IGenericRepository<,>)).InstancePerLifetimeScope();
        
        var apiAssembly = Assembly.GetExecutingAssembly();

        var repoAssembly = Assembly.GetAssembly(typeof(MealMateAPIDbContext)); 

        var serviceAssembly = Assembly.GetAssembly(typeof(MapperProfile));

        containerBuilder.RegisterAssemblyTypes(apiAssembly, repoAssembly)
            .Where(x=> x.Name.EndsWith("Repository"))
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope(); 

        containerBuilder.RegisterGeneric(typeof(BaseService<,>)).As(typeof(IBaseService<,>)).InstancePerLifetimeScope();

        containerBuilder.RegisterAssemblyTypes(serviceAssembly, apiAssembly)
            .Where(x=>x.Name.EndsWith("Service"))
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();
    }
}
