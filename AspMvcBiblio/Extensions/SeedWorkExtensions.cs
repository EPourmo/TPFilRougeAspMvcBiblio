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

			services.AddScoped(
				typeof(IBookRepository),
				typeof(BookRepository));

			services.AddScoped(
				typeof(IAuthorRepository),
				typeof(AuthorRepository));

			return services;
        }
    }
}
