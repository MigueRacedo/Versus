using Versus.ViewModels;

namespace Versus.Interfaces;

public interface ICompetitorService
{
    Task<IEnumerable<CompetitorViewModel>> GetAllAsync();
    Task<CompetitorViewModel?> GetByIdAsync(int id);
    Task CreateAsync(CompetitorViewModel viewModel);
    Task UpdateAsync(CompetitorViewModel viewModel);
    Task DeleteAsync(int id);
    Task AssignCategoryAsync(int competitorId, int? categoryId);
    Task<IEnumerable<CompetitorViewModel>> GetByCategoryAsync(int categoryId);
}
