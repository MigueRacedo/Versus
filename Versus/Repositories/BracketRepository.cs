using Microsoft.EntityFrameworkCore;
using Versus.Data;
using Versus.Interfaces;
using Versus.Models;

namespace Versus.Repositories;

public class BracketRepository : IBracketRepository
{
    private readonly ApplicationDbContext _context;

    public BracketRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Bracket?> GetByIdAsync(int id)
        => await _context.Brackets
            .Include(b => b.Matches)
                .ThenInclude(m => m.Competitor1)
            .Include(b => b.Matches)
                .ThenInclude(m => m.Competitor2)
            .Include(b => b.Matches)
                .ThenInclude(m => m.Winner)
            .FirstOrDefaultAsync(b => b.Id == id);

    public async Task<Bracket?> GetByCompetitionAsync(int competitionId)
        => await _context.Brackets
            .Include(b => b.Matches)
                .ThenInclude(m => m.Competitor1)
            .Include(b => b.Matches)
                .ThenInclude(m => m.Competitor2)
            .Include(b => b.Matches)
                .ThenInclude(m => m.Winner)
            .FirstOrDefaultAsync(b => b.CompetitionId == competitionId);

    public async Task AddAsync(Bracket bracket)
    {
        _context.Brackets.Add(bracket);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Bracket bracket)
    {
        _context.Brackets.Update(bracket);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var bracket = await _context.Brackets.FindAsync(id);
        if (bracket != null)
        {
            _context.Brackets.Remove(bracket);
            await _context.SaveChangesAsync();
        }
    }
}
