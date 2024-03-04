namespace StarRailAPI.Data
{
    public class Character
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Avatar { get; set; }

        public int? DestinyId { get; set; }
        public Destiny Destiny { get; set; } = null!;

        public int? SystemDataId { get; set; }
        public SystemData SystemData { get; set; } = null!;
    }
}