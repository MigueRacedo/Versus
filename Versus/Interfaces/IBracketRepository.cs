using Versus.Models;

namespace Versus.Interfaces;

public interface IBracketRepository
{
    Task<Bracket?> GetByIdAsync(int id);
    Task<Bracket?> GetByCompetitionAsync(int competitionId);
    Task AddAsync(Bracket bracket);
    Task UpdateAsync(Bracket bracket);
    Task DeleteAsync(int id);
}
