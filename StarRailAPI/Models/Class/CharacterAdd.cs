using StarRailAPI.Models.Interface;

namespace StarRailAPI.Models.Class
{
    public class CharacterAdd : ICharacter
    {
        public required string Name { get; set; }
        public IFormFile? Image { get; set; }
        public required string DestinyName { get; set; }
        public required string SystemName { get; set; }
    }
}