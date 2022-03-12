using GeoBaseSearch.Infrastructure.DataAccess;
using GeoBaseSearch.Infrastructure.DataAccess.Abstract;
using Microsoft.Extensions.DependencyInjection;

namespace GeoBaseSearch.Infrastructure.DependencyInjection
{
	public static class DependencyRegistrar
	{
		public static IServiceCollection AddInfrastructureLevelServices(this IServiceCollection serviceCollection)
		{
			serviceCollection.AddSingleton<IInMemoryDatabase, InMemoryDatabase>();
			serviceCollection.AddSingleton<IGeoBaseLoader, GeoBaseFileLoader>();
			serviceCollection.AddSingleton<IGeoBaseImageParser, GeoBaseImageParser>();

			return serviceCollection;
		}
	}
}
