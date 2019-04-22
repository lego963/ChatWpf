using ChatWpf.Core.DI.Interfaces;
using Dna;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ChatWpf.Relational
{
    public static class FrameworkConstructionExtensions
    {
        public static FrameworkConstruction AddClientDataStore(this FrameworkConstruction construction)
        {
            // Inject our SQLite EF data store
            construction.Services.AddDbContext<ClientDataStoreDbContext>(options =>
            {

                options.UseSqlite("Data Source = Synthesis.db");
                //construction.Configuration.GetConnectionString("ClientDataStoreConnection") == Data Source = synthesis.db!!!!!!!!!!!!!!!
            }, contextLifetime: ServiceLifetime.Transient);

            construction.Services.AddTransient<IClientDataStore>(
                provider => new BaseClientDataStore(provider.GetService<ClientDataStoreDbContext>()));

            return construction;
        }
    }
}
