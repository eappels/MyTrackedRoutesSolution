using Microsoft.EntityFrameworkCore;
using MyTrackedRoutes.Models;

namespace MyTrackedRoutes.Helpers;

public class AppDbContext : DbContext
{
    public DbSet<CustomTrack> Tracks { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string dbPath = Path.Combine(FileSystem.AppDataDirectory, "CustomTracks.sqlite");
        optionsBuilder.UseSqlite($"Filename={dbPath}");
    }
}