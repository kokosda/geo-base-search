using GeoBaseSearch.Domain.Locations;
using GeoBaseSearch.Infrastructure.DataAccess;
using GeoBaseSearch.Infrastructure.DataAccess.Abstract;
using GeoBaseSearch.Infrastructure.IpAddresses;
using GeoBaseSearch.Infrastructure.IpAddresses.Interfaces;
using GeoBaseSearch.Infrastructure.Locations;
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
			serviceCollection.AddSingleton<IIpAddressConverter, IpAddressConverter>();
			serviceCollection.AddSingleton<ILocationRepository, LocationInMemoryRepository>();

			return serviceCollection;
		}
	}
}
