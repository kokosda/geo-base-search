using GeoBaseSearch.Core.Interfaces;

namespace GeoBaseSearch.Infrastructure.IpAddresses.Interfaces;

public interface IIpAddressConverter
{
	IResponseContainerWithValue<uint> ConvertStringToUInt32IpAddress(string ipAddressString);
}