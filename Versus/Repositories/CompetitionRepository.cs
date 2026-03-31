using Microsoft.EntityFrameworkCore;
using Versus.Data;
using Versus.Interfaces;
using Versus.Models;

namespace Versus.Repositories;

public class CompetitionRepository : ICompetitionRepository
{
    private readonly ApplicationDbContext _context;

    public CompetitionRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Competition>> GetAllAsync()
        => await _context.Competitions.Include(c => c.Category).Include(c => c.Brackets).ToListAsync();

    public async Task<Competition?> GetByIdAsync(int id)
        => await _context.Competitions.Include(c => c.Category).Include(c => c.Brackets).FirstOrDefaultAsync(c => c.Id == id);

    public async Task AddAsync(Competition competition)
    {
        _context.Competitions.Add(competition);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Competition competition)
    {
        _context.Competitions.Update(competition);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var competition = await _context.Competitions.FindAsync(id);
        if (competition != null)
        {
            _context.Competitions.Remove(competition);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> ExistsAsync(int id)
        => await _context.Competitions.AnyAsync(c => c.Id == id);
}
