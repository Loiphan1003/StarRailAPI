namespace StarRailAPI.Models.Interface
{
    public interface ICharacter
    {
        string Name { get; set; }
        IFormFile? Image { get; set; }
        string? DestinyName { get; set; }
        string? SystemName { get; set; }
    }
}