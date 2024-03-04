namespace StarRailAPI.Models
{
    public class DestinyAdd
    {
        public required string Name { get; set; }
        public IFormFile? Image { get; set; }
    }
}