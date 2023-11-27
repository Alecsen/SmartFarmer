using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EfcDataAccess;

public class SmartFarmerAppContext : DbContext
{
    public SmartFarmerAppContext(DbContextOptions<SmartFarmerAppContext> options)
        : base(options)
    {
    }
    
    public DbSet<User> Users { get; set; }
    public DbSet<Field> Fields { get; set; }
    
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite("Data Source = ../EfcDataAccess/SmartFarmer.db");
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasKey(user => user.Id);
        modelBuilder.Entity<Field>().HasKey(field => field.Id);
        
        
        modelBuilder.Entity<User>().HasData(DatabaseInitializer.GetAuthenticationUsers());
        modelBuilder.Entity<Field>().HasData(DatabaseInitializer.GetFields());
        
    }
}