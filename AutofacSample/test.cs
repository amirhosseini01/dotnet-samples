using Autofac;

namespace AutofacSample;

public class Test
{
    public test()
    {
        var builder = new ContainerBuilder();

        builder.Register(c => new TaskController(c.Resolve<ITaskRepository>()));
        builder.RegisterType<TaskController>();
        builder.RegisterInstance(new TaskController());
        builder.RegisterAssemblyTypes(controllerAssembly);

        var container = builder.Build();
    }
}