using CategoryWebApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace CategoryWebApi.Data;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<CategoryEntity> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CategoryEntity>()
            .HasData(
                new CategoryEntity { Id = 1, Description = "Wellness & Relax" },
                new CategoryEntity { Id = 2, Description = "Workout & Gym" },
                new CategoryEntity { Id = 3, Description = "Festival & Fun" },
                new CategoryEntity { Id = 4, Description = "Food & Drinks" },
                new CategoryEntity { Id = 5, Description = "Travel & Adventure" },
                new CategoryEntity { Id = 6, Description = "Sports & Outdoors" },
                new CategoryEntity { Id = 7, Description = "Music & Concerts" },
                new CategoryEntity { Id = 8, Description = "Arts & Crafts" },
                new CategoryEntity { Id = 9, Description = "Education & Learning" },
                new CategoryEntity { Id = 10, Description = "Technology & Gadgets" }
            );
    }
}
