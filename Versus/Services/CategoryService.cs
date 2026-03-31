using Versus.Interfaces;
using Versus.Models;
using Versus.ViewModels;

namespace Versus.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _repo;

    public CategoryService(ICategoryRepository repo)
    {
        _repo = repo;
    }

    public async Task<IEnumerable<CategoryViewModel>> GetAllAsync()
    {
        var categories = await _repo.GetAllAsync();
        return categories.Select(MapToViewModel);
    }

    public async Task<CategoryViewModel?> GetByIdAsync(int id)
    {
        var category = await _repo.GetByIdAsync(id);
        return category == null ? null : MapToViewModel(category);
    }

    public async Task CreateAsync(CategoryViewModel vm)
    {
        var category = new Category
        {
            Name = vm.Name,
            Description = vm.Description,
            MinWeight = vm.MinWeight,
            MaxWeight = vm.MaxWeight,
            MinAge = vm.MinAge,
            MaxAge = vm.MaxAge
        };
        await _repo.AddAsync(category);
    }

    public async Task UpdateAsync(CategoryViewModel vm)
    {
        var category = await _repo.GetByIdAsync(vm.Id);
        if (category == null) return;
        category.Name = vm.Name;
        category.Description = vm.Description;
        category.MinWeight = vm.MinWeight;
        category.MaxWeight = vm.MaxWeight;
        category.MinAge = vm.MinAge;
        category.MaxAge = vm.MaxAge;
        await _repo.UpdateAsync(category);
    }

    public async Task DeleteAsync(int id) => await _repo.DeleteAsync(id);

    private static CategoryViewModel MapToViewModel(Category c) => new()
    {
        Id = c.Id,
        Name = c.Name,
        Description = c.Description,
        MinWeight = c.MinWeight,
        MaxWeight = c.MaxWeight,
        MinAge = c.MinAge,
        MaxAge = c.MaxAge,
        CompetitorCount = c.Competitors.Count
    };
}
