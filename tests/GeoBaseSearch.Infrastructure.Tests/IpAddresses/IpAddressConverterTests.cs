using GeoBaseSearch.Infrastructure.IpAddresses;
using NUnit.Framework;

namespace GeoBaseSearch.Infrastructure.Tests.IpAddresses;

[TestFixture]
public sealed class IpAddressConverterTests
{
	private readonly IpAddressConverter _ipAddressConverter = new();

	[TestCase("192.168.1.1", 16885952, TestName = "When IP address is well-formed, then return IP address INT 32 representation. IPv4 192.168.1.1")]
	[TestCase("8.8.8.8", 134744072, TestName = "When IP address is well-formed, then return IP address INT 32 representation. IPv4 8.8.8.8")]
	public void ConvertStringToInt32IpAddress_WhenIpAddressStringIsWellFormed_ReturnsIpAddressInt32Representation(string ipAddressString, int expectedIpAddress)
	{
		// Act
		var result = _ipAddressConverter.ConvertStringToInt32IpAddress(ipAddressString);

		// Assert
		Assert.IsNotNull(result);
		Assert.That(result.IsSuccess, Is.True);
		Assert.AreEqual(expectedIpAddress, result.Value);
	}

	[TestCase("2001:0db8:85a3:0000:0000:8a2e:0370:7334", TestName = "When IP address is IP v6, then return error message.")]
	public void ConvertStringToInt32IpAddress_WhenIpAddressStringIsIpV6_ReturnsErrorMessage(string ipAddressString)
	{
		// Act
		var result = _ipAddressConverter.ConvertStringToInt32IpAddress(ipAddressString);

		// Assert
		Assert.IsNotNull(result);
		Assert.That(result.IsSuccess, Is.False);
	}

	[TestCase("152.15.12", TestName = "When IP address is malformed, then return error message. 152.15.12")]
	[TestCase("440.15.12.1", TestName = "When IP address is malformed, then return error message. 440.15.12.1")]
	public void ConvertStringToInt32IpAddress_WhenIpAddressStringIsMalformed_ReturnsErrorMessage(string ipAddressString)
	{
		// Act
		var result = _ipAddressConverter.ConvertStringToInt32IpAddress(ipAddressString);

		// Assert
		Assert.IsNotNull(result);
		Assert.That(result.IsSuccess, Is.False);
	}
}