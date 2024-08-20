using System;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Entities;

namespace WebApplication1.Data;

public class GameStoreContext(DbContextOptions<GameStoreContext> options) : DbContext(options)
{
    // DbSet representing the Games table
    public DbSet<Game> Games => Set<Game>();

    // DbSet representing the Genre table
    public DbSet<Genre> Genre => Set<Genre>();

    // Configures the model properties and seed data
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Seed data for the Genre table
        modelBuilder.Entity<Genre>().HasData(
            new {Id = 1, Name = "Fighting"},
            new {Id = 2, Name = "RPG"},
            new {Id = 3, Name = "Sports"},
            new {Id = 4, Name = "Racing"}
        );
    }
}
