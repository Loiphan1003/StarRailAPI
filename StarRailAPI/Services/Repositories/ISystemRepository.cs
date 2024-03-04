using StarRailAPI.Common;
using StarRailAPI.Data;
using StarRailAPI.Models;

namespace StarRailAPI.Service.Repositories
{
    public interface ISystemRepository
    {
        List<SystemData> Get();
        Task<Result> Add(SystemAdd model);
        Task<Result> Update(SystemUpdate model);
        Task<Result> Remove(int id);
    }
}