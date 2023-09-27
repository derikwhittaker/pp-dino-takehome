namespace PrizePicks.API;

using Autofac;
using PrizePicks.API.Data;
using PrizePicks.API.Rules;
using PrizePicks.API.Services;

public class Registration : Module
{
    /// <summary>
    /// Autofac registration here.  As new services are created, update them below
    ///
    /// Could have gone the route of auto registration, maybe next time.
    /// </summary>
    /// <param name="builder"></param>
    protected override void Load(ContainerBuilder builder)
    {
        // Services
        builder.RegisterType<CageService>().As<ICageService>();
        builder.RegisterType<DinosaurService>().As<IDinosaurService>();

        // Rules
        builder.RegisterType<CageRules>().As<ICageRules>();
        builder.RegisterType<DinosaurRules>().As<IDinosaurRules>();

        // Data Registration
        builder.RegisterType<CageRepository>().As<ICageRepository>();
        builder.RegisterType<DinosaurRepository>().As<IDinosaurRepository>();

        // This is what would change if we had a real DB backing store, not inmemory one
        builder.RegisterType<InMemoryDB>().As<IDatabase>();
    }
}
