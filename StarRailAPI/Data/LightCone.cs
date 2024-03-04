namespace StarRailAPI.Data
{
    public class LightCone
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public string? Logo { get; set; }
        public int? DestinyId { get; set; }
        public Destiny Destiny { get; set; } = null!;
    }
}