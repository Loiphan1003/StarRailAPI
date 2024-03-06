using StarRailAPI.Models.Interface;

namespace StarRailAPI.Models.Class
{
    public class CharacterUpdate : ICharacter
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public IFormFile? Image { get; set; }
        public string? DestinyName { get; set; }
        public string? SystemName { get; set; }
    }
}