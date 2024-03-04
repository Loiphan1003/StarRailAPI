using Name;
using StarRailAPI.Common;
using StarRailAPI.Data;

namespace StarRailAPI.Service.Repositories
{
    public interface ICharacterRepository
    {
        List<Character> Get();

        // Task<Result> Add(CharacterAdd model);
    }
}