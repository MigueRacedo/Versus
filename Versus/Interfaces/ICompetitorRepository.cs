using Versus.Models;

namespace Versus.Interfaces;

public interface ICompetitorRepository
{
    Task<IEnumerable<Competitor>> GetAllAsync();
    Task<Competitor?> GetByIdAsync(int id);
    Task<IEnumerable<Competitor>> GetByCategoryAsync(int categoryId);
    Task AddAsync(Competitor competitor);
    Task UpdateAsync(Competitor competitor);
    Task DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
}
