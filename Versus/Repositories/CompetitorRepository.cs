using Microsoft.EntityFrameworkCore;
using Versus.Data;
using Versus.Interfaces;
using Versus.Models;

namespace Versus.Repositories;

public class CompetitorRepository : ICompetitorRepository
{
    private readonly ApplicationDbContext _context;

    public CompetitorRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Competitor>> GetAllAsync()
        => await _context.Competitors.Include(c => c.Category).ToListAsync();

    public async Task<Competitor?> GetByIdAsync(int id)
        => await _context.Competitors.Include(c => c.Category).FirstOrDefaultAsync(c => c.Id == id);

    public async Task<IEnumerable<Competitor>> GetByCategoryAsync(int categoryId)
        => await _context.Competitors.Include(c => c.Category).Where(c => c.CategoryId == categoryId).ToListAsync();

    public async Task AddAsync(Competitor competitor)
    {
        _context.Competitors.Add(competitor);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Competitor competitor)
    {
        _context.Competitors.Update(competitor);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var competitor = await _context.Competitors.FindAsync(id);
        if (competitor != null)
        {
            _context.Competitors.Remove(competitor);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> ExistsAsync(int id)
        => await _context.Competitors.AnyAsync(c => c.Id == id);
}
