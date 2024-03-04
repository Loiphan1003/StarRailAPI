namespace StarRailAPI.Data
{
    public class Destiny
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Logo { get; set; }
        public ICollection<LightCone> LightCones { get; } = new List<LightCone>();
        public ICollection<Character> Characters { get; } = new List<Character>();
    }
}