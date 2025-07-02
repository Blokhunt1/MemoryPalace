using Microsoft.EntityFrameworkCore;
using MemoryPalaceApp.Data;
using MemoryPalaceApp.Models;

namespace MemoryPalaceApp.Services;

public class MemoryPalaceService
{
    private readonly MemoryPalaceContext _context;

    public MemoryPalaceService(MemoryPalaceContext context)
    {
        _context = context;
    }

    public async Task<List<MemoryPalace>> GetAllMemoryPalacesAsync()
    {
        return await _context.MemoryPalaces
            .Include(mp => mp.PointsOfLoci)
            .ToListAsync();
    }

    public async Task<MemoryPalace?> GetMemoryPalaceAsync(int id)
    {
        return await _context.MemoryPalaces
            .Include(mp => mp.PointsOfLoci.OrderBy(l => l.Order))
            .FirstOrDefaultAsync(mp => mp.Id == id);
    }

    public async Task<MemoryPalace> CreateMemoryPalaceAsync(string name, string subject, List<string> imagePaths)
    {
        var memoryPalace = new MemoryPalace
        {
            Name = name,
            Subject = subject,
            CreatedAt = DateTime.UtcNow
        };

        _context.MemoryPalaces.Add(memoryPalace);
        await _context.SaveChangesAsync();

        for (int i = 0; i < imagePaths.Count; i++)
        {
            var loci = new Loci
            {
                MemoryPalaceId = memoryPalace.Id,
                Order = i + 1,
                Location = $"Location {i + 1}",
                ImagePath = imagePaths[i]
            };
            _context.Loci.Add(loci);
        }

        await _context.SaveChangesAsync();
        return await GetMemoryPalaceAsync(memoryPalace.Id) ?? memoryPalace;
    }

    public async Task UpdateLociAsync(Loci loci)
    {
        _context.Loci.Update(loci);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteMemoryPalaceAsync(int id)
    {
        var memoryPalace = await _context.MemoryPalaces.FindAsync(id);
        if (memoryPalace != null)
        {
            _context.MemoryPalaces.Remove(memoryPalace);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<MemoryPalace?> GetRandomMemoryPalaceAsync()
    {
        var palaces = await _context.MemoryPalaces
            .Include(mp => mp.PointsOfLoci.OrderBy(l => l.Order))
            .Where(mp => mp.PointsOfLoci.Any())
            .ToListAsync();

        if (!palaces.Any()) return null;

        var random = new Random();
        return palaces[random.Next(palaces.Count)];
    }

    public async Task<bool> UpdateLociOrderAsync(int palaceId, List<int> newOrder)
    {
        var palace = await GetMemoryPalaceAsync(palaceId);
        if (palace == null) return false;

        for (int i = 0; i < newOrder.Count; i++)
        {
            var loci = palace.PointsOfLoci.FirstOrDefault(l => l.Id == newOrder[i]);
            if (loci != null)
            {
                loci.Order = i + 1;
                _context.Loci.Update(loci);
            }
        }

        await _context.SaveChangesAsync();
        return true;
    }
}