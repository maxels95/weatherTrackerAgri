using Microsoft.EntityFrameworkCore;
using AgriWeatherTracker.Models;

namespace AgriWeatherTracker.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Crop> Crops { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Weather> Weathers { get; set; }
        public DbSet<GrowthCycle> GrowthCycles { get; set; }
        public DbSet<GrowthStage> GrowthStages { get; set; }
        public DbSet<ConditionThreshold> ConditionThresholds { get; set; }
        public DbSet<HealthScore> HealthScores { get; set; }
        public DbSet<SignalGenerated> SignalsGenerated { get; set; } // Added DbSet for SignalGenerated

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure the one-to-many relationship between Crop and Location
            modelBuilder.Entity<Crop>()
                .HasMany(c => c.Locations)
                .WithOne(l => l.Crop)
                .HasForeignKey(l => l.CropId)
                .IsRequired(false);

            modelBuilder.Entity<Crop>()
                .HasMany(c => c.HealthScores)
                .WithOne(hs => hs.Crop)
                .HasForeignKey(hs => hs.CropId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired(false);

            modelBuilder.Entity<Location>().Property(p => p.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<Location>()
                .HasOne(l => l.Crop)
                .WithMany(c => c.Locations)
                .HasForeignKey(l => l.CropId)
                .IsRequired(false);

            modelBuilder.Entity<GrowthStage>()
                .Property(e => e.StartDate)
                .HasConversion(v => v.ToUniversalTime(), v => DateTime.SpecifyKind(v, DateTimeKind.Utc));

            modelBuilder.Entity<GrowthStage>()
                .Property(e => e.EndDate)
                .HasConversion(v => v.ToUniversalTime(), v => DateTime.SpecifyKind(v, DateTimeKind.Utc));

            modelBuilder.Entity<ConditionThreshold>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<GrowthStage>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<HealthScore>()
                .HasOne(hs => hs.Crop)
                .WithMany(c => c.HealthScores)
                .HasForeignKey(hs => hs.CropId);

            modelBuilder.Entity<HealthScore>()
                .Property(h => h.Score)
                .HasDefaultValue(0);

            modelBuilder.Entity<HealthScore>()
                .Property(hs => hs.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<HealthScore>()
                .HasOne(hs => hs.Location)
                .WithMany()
                .HasForeignKey(hs => hs.LocationId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<HealthScore>()
                .Property(hs => hs.Date)
                .HasConversion(v => v.ToUniversalTime(), v => DateTime.SpecifyKind(v, DateTimeKind.Utc));

            // Configure the SignalGenerated entity
            modelBuilder.Entity<SignalGenerated>()
                .Property(sg => sg.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<SignalGenerated>()
                .HasOne(sg => sg.Crop)
                .WithMany(c => c.SignalsGenerated)
                .HasForeignKey(sg => sg.CropId)
                .OnDelete(DeleteBehavior.SetNull)
                .IsRequired(false);

            modelBuilder.Entity<SignalGenerated>()
                .HasOne(sg => sg.Location)
                .WithMany(l => l.SignalsGenerated)
                .HasForeignKey(sg => sg.LocationId)
                .OnDelete(DeleteBehavior.SetNull)
                .IsRequired(false);

            modelBuilder.Entity<SignalGenerated>()
                .HasOne(sg => sg.HealthScore)
                .WithOne(hs => hs.SignalGenerated)
                .HasForeignKey<SignalGenerated>(sg => sg.HealthScoreId)
                .OnDelete(DeleteBehavior.SetNull)
                .IsRequired(false); // One-to-one relationship

            modelBuilder.Entity<SignalGenerated>()
                .HasOne(sg => sg.Weather)
                .WithOne(w => w.SignalGenerated)
                .HasForeignKey<SignalGenerated>(sg => sg.WeatherId)
                .OnDelete(DeleteBehavior.SetNull)
                .IsRequired(false); // One-to-one relationship

            modelBuilder.Entity<Weather>()
                .Property(w => w.SignalGeneratedId)
                .IsRequired(false); // Make the foreign key nullable
        }
    }
}
