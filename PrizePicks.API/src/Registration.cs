namespace PrizePicks.API;

using Autofac;
using PrizePicks.API.Data;
using PrizePicks.API.Rules;
using PrizePicks.API.Services;

public class Registration : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        // Services
        builder.RegisterType<CageService>().As<ICageService>();
        builder.RegisterType<DinosaurService>().As<IDinosaurService>();

        // Rules
        builder.RegisterType<CageRules>().As<ICageRules>();

        // Data Registration
        builder.RegisterType<CageRepository>().As<ICageRepository>();
        builder.RegisterType<DinosaurRepository>().As<IDinosaurRepository>();
        builder.RegisterType<InMemoryDB>().As<IDatabase>();
    }
}
