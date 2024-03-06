using StarRailAPI.Data;
using StarRailAPI.Models;

namespace StarRailAPI.Service.Repositories
{
    public interface ILightConeRepository
    {
        List<LightCone> Get();
        // Task<string> Remove(int id);
        Task<string> Update(LightConeUpdate model);
        Task<string> Add(LightConeAdd model);
    }
}