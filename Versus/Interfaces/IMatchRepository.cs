using Versus.Models;

namespace Versus.Interfaces;

public interface IMatchRepository
{
    Task<IEnumerable<Match>> GetByBracketAsync(int bracketId);
    Task<Match?> GetByIdAsync(int id);
    Task AddRangeAsync(IEnumerable<Match> matches);
    Task UpdateAsync(Match match);
    Task DeleteByBracketAsync(int bracketId);
}
