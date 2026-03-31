using Versus.ViewModels;

namespace Versus.Interfaces;

public interface ICompetitionService
{
    Task<IEnumerable<CompetitionViewModel>> GetAllAsync();
    Task<CompetitionViewModel?> GetByIdAsync(int id);
    Task CreateAsync(CompetitionViewModel viewModel);
    Task UpdateAsync(CompetitionViewModel viewModel);
    Task DeleteAsync(int id);
}
