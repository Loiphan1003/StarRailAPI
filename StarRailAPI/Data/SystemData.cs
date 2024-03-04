namespace StarRailAPI.Data
{
    public class SystemData
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Logo { get; set; }
        public ICollection<Character> Characters {get; } = null!;
    }
}