using System.ComponentModel.DataAnnotations;

namespace MemoryPalaceApp.Models;

public class Loci
{
    public int Id { get; set; }
    
    public int MemoryPalaceId { get; set; }
    
    public MemoryPalace MemoryPalace { get; set; } = null!;
    
    public int Order { get; set; }
    
    public string Category { get; set; } = string.Empty;
    
    public string Location { get; set; } = string.Empty;
    
    public string ImagePath { get; set; } = string.Empty;
    
    public string WrittenKey { get; set; } = string.Empty;
    
    public string ImageKey { get; set; } = string.Empty;
    
    public string WrittenValue { get; set; } = string.Empty;
    
    public string ImageValue { get; set; } = string.Empty;
}