using System.Net;
using GeoBaseSearch.Core.Interfaces;
using GeoBaseSearch.Core.ResponseContainers;
using GeoBaseSearch.Infrastructure.IpAddresses.Interfaces;

namespace GeoBaseSearch.Infrastructure.IpAddresses;

public sealed class IpAddressConverter : IIpAddressConverter
{
	private const int IPV4_SECTIONS_COUNT = 4;

	public IResponseContainerWithValue<int> ConvertStringToInt32IpAddress(string ipAddressString)
	{
		var result = new ResponseContainerWithValue<int>();

		if (!IPAddress.TryParse(ipAddressString, out var ipAddress))
		{
			result.AddErrorMessage($"Provided value {ipAddressString} can not be converted to IP address.");
			return result;
		}

		var ipAddressBytes = ipAddress.GetAddressBytes();

		if (ipAddressBytes.Length != IPV4_SECTIONS_COUNT)
		{
			result.AddErrorMessage($"Only IP v4 IP addresses are supported. Resulting IP address form is {ipAddress}");
			return result;
		}

		var ipAddressInt32 = BitConverter.ToInt32(new ReadOnlySpan<byte>(ipAddressBytes));
		result.SetSuccessValue(ipAddressInt32);
		return result;
	}
}