using System.ComponentModel.DataAnnotations;

namespace MemoryPalaceApp.Models;

public class MemoryPalace
{
    public int Id { get; set; }
    
    [Required]
    public string Name { get; set; } = string.Empty;
    
    public string Subject { get; set; } = string.Empty;
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public List<Loci> PointsOfLoci { get; set; } = new();
}