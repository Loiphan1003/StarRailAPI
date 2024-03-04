namespace StarRailAPI.Models
{
    public class SystemUpdate
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public IFormFile? Image { get; set; }
    }
}