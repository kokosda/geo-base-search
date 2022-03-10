using GeoBaseSearch.Core.Interfaces;

namespace GeoBaseSearch.Core.Handlers
{
    public interface IQueryHandler<in T>
    {
        Task<IResponseContainer> HandleAsync(T query);
    }
}