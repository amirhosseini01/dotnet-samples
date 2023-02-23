using System.Reflection;
using Autofac;
using Autofac.Integration.WebApi;
using AutofacSample.Services;

namespace AutofacSample;

public class AutofacBusinessModule : Autofac.Module
{
    protected override void Load(ContainerBuilder builder)
    {
        // var config = GlobalConfiguration.Configuration;
        
          builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
        
        builder.RegisterType<SingleService>().As<ISingleService>().SingleInstance();
        builder.RegisterType<TransientService>().As<ITransientService>().InstancePerDependency();
        builder.RegisterType<TransientService>().As<ITransientService>().InstancePerLifetimeScope();


        //  var container = builder.Build();
        // config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
    }
}