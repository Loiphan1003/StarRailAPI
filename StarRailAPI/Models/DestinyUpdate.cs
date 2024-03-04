namespace StarRailAPI.Models
{
    public class DestinyUpdate
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public IFormFile? Image { get; set; }
    }
}