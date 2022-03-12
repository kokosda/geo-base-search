using GeoBaseSearch.Application.Cities;
using GeoBaseSearch.Application.Locations;
using GeoBaseSearch.Core.Handlers;
using Microsoft.Extensions.DependencyInjection;

namespace GeoBaseSearch.Application.DependencyInjection
{
	public static class DependencyRegistrar
	{
		public static IServiceCollection AddApplicationLevelServices(this IServiceCollection serviceCollection)
		{
			serviceCollection.AddScoped<IGenericQueryHandler<LocationsByCityQuery, LocationsByCityDto>>();
			serviceCollection.AddScoped<IGenericQueryHandler<LocationByIpQuery, LocationByIpDto>>();
			return serviceCollection;
		}
	}
}
