namespace StarRailAPI.Models
{
    public class LightConeAdd
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public int? DestinyId { get; set; }
        public IFormFile? Image { get; set; }
    }
}