using System.Net;
using GeoBaseSearch.Core.Interfaces;
using GeoBaseSearch.Core.ResponseContainers;
using GeoBaseSearch.Infrastructure.IpAddresses.Interfaces;

namespace GeoBaseSearch.Infrastructure.IpAddresses;

public sealed class IpAddressConverter : IIpAddressConverter
{
	private const int IPV4_SECTIONS_COUNT = 4;

	public IResponseContainerWithValue<uint> ConvertStringToUInt32IpAddress(string ipAddressString)
	{
		var result = new ResponseContainerWithValue<uint>();

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

		var ipAddressUInt32 = BitConverter.ToUInt32(new ReadOnlySpan<byte>(ipAddressBytes));
		result.SetSuccessValue(ipAddressUInt32);
		return result;
	}
}