using StarRailAPI.Common;
using StarRailAPI.Data;
using StarRailAPI.Models;

namespace StarRailAPI.Service.Repositories
{
    public interface IDestinyRepository
    {
        List<Destiny> Get();
        Task<Result> Add(DestinyAdd model);
        Task<string> Update(DestinyUpdate model);
        Task<string> Remove(int id);
    }
}