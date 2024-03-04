namespace StarRailAPI.Models
{
    public class LightConeUpdate
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        public string? Description { get; set; }
        public IFormFile? Image { get; set; }
        public int? DestinyId { get; set; }
    }
}