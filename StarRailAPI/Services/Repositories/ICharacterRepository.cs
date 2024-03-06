using StarRailAPI.Common;
using StarRailAPI.Data;
using StarRailAPI.Models.Class;

namespace StarRailAPI.Service.Repositories
{
    public interface ICharacterRepository
    {
        List<Character> Get();

        Task<Result> Add(CharacterAdd model);
        Task<Result> Remove(string name);
        Task<Result> Update(CharacterUpdate model);
    }
}