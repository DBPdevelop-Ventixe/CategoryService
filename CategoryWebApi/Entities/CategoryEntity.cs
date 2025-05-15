using System.ComponentModel.DataAnnotations;

namespace CategoryWebApi.Entities;

public class CategoryEntity
{
    [Key]
    public int Id { get; set; }
    public string Description { get; set; } = null!;
}
