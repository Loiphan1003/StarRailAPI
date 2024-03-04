namespace StarRailAPI.Models
{
    public class SystemAdd
    {
        public required string Name { get; set; }
        public IFormFile? Image { get; set; }
    }
}