using Versus.ViewModels;

namespace Versus.Interfaces;

public interface ICategoryService
{
    Task<IEnumerable<CategoryViewModel>> GetAllAsync();
    Task<CategoryViewModel?> GetByIdAsync(int id);
    Task CreateAsync(CategoryViewModel viewModel);
    Task UpdateAsync(CategoryViewModel viewModel);
    Task DeleteAsync(int id);
}
