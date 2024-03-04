using Name;
using StarRailAPI.Common;
using StarRailAPI.Data;

namespace StarRailAPI.Service.Repositories
{
    public class CharacterRepository : ICharacterRepository
    {
        private readonly StarRailContext _context;

        public CharacterRepository(StarRailContext context)
        {
            _context = context;
        }

        // public Task<Result> Add(CharacterAdd model)
        // {

        // }

        public List<Character> Get()
        {
            var characters = _context.Characters.ToList();

            return characters;
        }
    }
}