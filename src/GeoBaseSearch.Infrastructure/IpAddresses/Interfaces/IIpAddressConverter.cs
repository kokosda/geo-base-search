using GeoBaseSearch.Core.Interfaces;

namespace GeoBaseSearch.Infrastructure.IpAddresses.Interfaces;

public interface IIpAddressConverter
{
	IResponseContainerWithValue<int> ConvertStringToInt32IpAddress(string ipAddressString);
}