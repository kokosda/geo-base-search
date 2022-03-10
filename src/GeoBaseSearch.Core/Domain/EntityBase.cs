namespace GeoBaseSearch.Core.Domain
{
	public abstract class EntityBase<TId>
	{
		public TId? Id { get; init; }
	}
}
