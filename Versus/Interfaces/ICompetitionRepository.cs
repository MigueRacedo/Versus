using Versus.Models;

namespace Versus.Interfaces;

public interface ICompetitionRepository
{
    Task<IEnumerable<Competition>> GetAllAsync();
    Task<Competition?> GetByIdAsync(int id);
    Task AddAsync(Competition competition);
    Task UpdateAsync(Competition competition);
    Task DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
}
