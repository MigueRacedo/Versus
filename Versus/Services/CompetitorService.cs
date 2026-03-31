using Versus.Interfaces;
using Versus.Models;
using Versus.ViewModels;

namespace Versus.Services;

public class CompetitorService : ICompetitorService
{
    private readonly ICompetitorRepository _repo;

    public CompetitorService(ICompetitorRepository repo)
    {
        _repo = repo;
    }

    public async Task<IEnumerable<CompetitorViewModel>> GetAllAsync()
    {
        var competitors = await _repo.GetAllAsync();
        return competitors.Select(MapToViewModel);
    }

    public async Task<CompetitorViewModel?> GetByIdAsync(int id)
    {
        var competitor = await _repo.GetByIdAsync(id);
        return competitor == null ? null : MapToViewModel(competitor);
    }

    public async Task CreateAsync(CompetitorViewModel vm)
    {
        var competitor = new Competitor
        {
            Name = vm.Name,
            Age = vm.Age,
            Weight = vm.Weight,
            CategoryId = vm.CategoryId
        };
        await _repo.AddAsync(competitor);
    }

    public async Task UpdateAsync(CompetitorViewModel vm)
    {
        var competitor = await _repo.GetByIdAsync(vm.Id);
        if (competitor == null) return;
        competitor.Name = vm.Name;
        competitor.Age = vm.Age;
        competitor.Weight = vm.Weight;
        competitor.CategoryId = vm.CategoryId;
        await _repo.UpdateAsync(competitor);
    }

    public async Task DeleteAsync(int id) => await _repo.DeleteAsync(id);

    public async Task AssignCategoryAsync(int competitorId, int? categoryId)
    {
        var competitor = await _repo.GetByIdAsync(competitorId);
        if (competitor == null) return;
        competitor.CategoryId = categoryId;
        await _repo.UpdateAsync(competitor);
    }

    public async Task<IEnumerable<CompetitorViewModel>> GetByCategoryAsync(int categoryId)
    {
        var competitors = await _repo.GetByCategoryAsync(categoryId);
        return competitors.Select(MapToViewModel);
    }

    private static CompetitorViewModel MapToViewModel(Competitor c) => new()
    {
        Id = c.Id,
        Name = c.Name,
        Age = c.Age,
        Weight = c.Weight,
        CategoryId = c.CategoryId,
        CategoryName = c.Category?.Name
    };
}
