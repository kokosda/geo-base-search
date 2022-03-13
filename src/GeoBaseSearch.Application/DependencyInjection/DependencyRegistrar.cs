using GeoBaseSearch.Application.Cities;
using GeoBaseSearch.Application.IpAddresses;
using GeoBaseSearch.Core.Handlers;
using Microsoft.Extensions.DependencyInjection;

namespace GeoBaseSearch.Application.DependencyInjection
{
	public static class DependencyRegistrar
	{
		public static IServiceCollection AddApplicationLevelServices(this IServiceCollection serviceCollection)
		{
			serviceCollection.AddScoped<IGenericQueryHandler<LocationsByCityQuery, LocationsByCityDto>, LocationsByCitiesQueryHandler>();
			serviceCollection.AddScoped<IGenericQueryHandler<LocationByIpQuery, LocationByIpDto>, LocationByIpQueryHandler>();
			return serviceCollection;
		}
	}
}
