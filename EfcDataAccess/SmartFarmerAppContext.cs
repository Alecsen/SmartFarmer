using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EfcDataAccess;

public class SmartFarmerAppContext : DbContext
{
    public DbSet<AuthenticationUser> Users { get; set; }
    
    public DbSet<Field> Fields { get; set; }
    public DbSet<Sensor> Sensors { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source = ../EfcDataAccess/SmartFarmer.db");
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);            
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AuthenticationUser>().HasKey(user => user.Id);
        modelBuilder.Entity<Field>().HasKey(field => field.Id);
        modelBuilder.Entity<Sensor>().HasKey(sensor => sensor.Id);
    }
}