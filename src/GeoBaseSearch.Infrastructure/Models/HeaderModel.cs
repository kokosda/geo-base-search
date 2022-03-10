namespace GeoBaseSearch.Infrastructure.Models;

public sealed class HeaderModel
{
	/// <summary>
	/// database version
	/// </summary>
	public int Version { get; init; }

	/// <summary>
	/// name/prefix for the database
	/// </summary>
	public sbyte[] Name { get; init; } = Array.Empty<sbyte>();

	/// <summary>
	/// database creation time
	/// </summary>
	public ulong Timestamp { get; init; }

	/// <summary>
	/// total number of entries
	/// </summary>
	public int Records { get; init; }

	/// <summary>
	/// offset from the beginning of the file to the beginning of the list of records with geo-information
	/// </summary>
	public uint OffsetRanges { get; init; }

	/// <summary>
	/// offset from the beginning of the file to the beginning of the index, sorted by city names
	/// </summary>
	public uint OffsetCities { get; init; }

	/// <summary>
	/// offset from the beginning of the file to the beginning of the list of location records
	/// </summary>
	public uint OffsetLocations { get; init; }
}