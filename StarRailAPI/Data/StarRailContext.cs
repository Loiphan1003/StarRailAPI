
using Microsoft.EntityFrameworkCore;

namespace StarRailAPI.Data
{
    public class StarRailContext : DbContext
    {
        public StarRailContext(DbContextOptions<StarRailContext> options) : base(options)
        {

        }


        #region DbSet
        public DbSet<Destiny> Destinies { get; set; }
        public DbSet<LightCone> LightCones { get; set; }
        public DbSet<SystemData> Systems { get; set; }
        public DbSet<Character> Characters { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Destiny>()
                .HasMany(e => e.LightCones)
                .WithOne(e => e.Destiny)
                .HasForeignKey(e => e.DestinyId)
                .IsRequired();

            modelBuilder.Entity<Destiny>()
                .HasMany(e => e.Characters)
                .WithOne(e => e.Destiny)
                .HasForeignKey(e => e.DestinyId)
                .IsRequired();

            modelBuilder.Entity<SystemData>()
                .HasMany(e => e.Characters)
                .WithOne(e => e.SystemData)
                .HasForeignKey(e => e.SystemDataId)
                .IsRequired();

            base.OnModelCreating(modelBuilder);
        }
    }
}