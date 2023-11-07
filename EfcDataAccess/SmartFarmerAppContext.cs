using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EfcDataAccess;

public class SmartFarmerAppContext : DbContext
{
    public DbSet<AuthenticationUser> Users { get; set; }
    
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source = ../EfcDataAccess/PostApp.db");
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);            
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AuthenticationUser>().HasKey(user => user.Id);
    }
}