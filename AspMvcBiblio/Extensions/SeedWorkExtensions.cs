using AspMvcBiblio.Data;

namespace AspMvcBiblio.Extensions
{
    public static class SeedWorkExtensions
    {
        public static IServiceCollection AddSeedwork(this IServiceCollection services)
        {
            services.AddScoped(
                typeof(IRepository<>),
                typeof(Repository<>));
            return services;
        }
    }
}
