using CategoryWebApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace CategoryWebApi.Data;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<CategoryEntity> Categories { get; set; }
}
