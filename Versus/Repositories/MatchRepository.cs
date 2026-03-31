using Microsoft.EntityFrameworkCore;
using Versus.Data;
using Versus.Interfaces;
using Versus.Models;

namespace Versus.Repositories;

public class MatchRepository : IMatchRepository
{
    private readonly ApplicationDbContext _context;

    public MatchRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Match>> GetByBracketAsync(int bracketId)
        => await _context.Matches
            .Include(m => m.Competitor1)
            .Include(m => m.Competitor2)
            .Include(m => m.Winner)
            .Where(m => m.BracketId == bracketId)
            .OrderBy(m => m.Round).ThenBy(m => m.MatchNumber)
            .ToListAsync();

    public async Task<Match?> GetByIdAsync(int id)
        => await _context.Matches
            .Include(m => m.Competitor1)
            .Include(m => m.Competitor2)
            .Include(m => m.Winner)
            .FirstOrDefaultAsync(m => m.Id == id);

    public async Task AddRangeAsync(IEnumerable<Match> matches)
    {
        _context.Matches.AddRange(matches);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Match match)
    {
        _context.Matches.Update(match);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteByBracketAsync(int bracketId)
    {
        var matches = await _context.Matches.Where(m => m.BracketId == bracketId).ToListAsync();
        _context.Matches.RemoveRange(matches);
        await _context.SaveChangesAsync();
    }
}
