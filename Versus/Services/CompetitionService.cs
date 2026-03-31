using Versus.Interfaces;
using Versus.Models;
using Versus.ViewModels;

namespace Versus.Services;

public class CompetitionService : ICompetitionService
{
    private readonly ICompetitionRepository _repo;

    public CompetitionService(ICompetitionRepository repo)
    {
        _repo = repo;
    }

    public async Task<IEnumerable<CompetitionViewModel>> GetAllAsync()
    {
        var competitions = await _repo.GetAllAsync();
        return competitions.Select(MapToViewModel);
    }

    public async Task<CompetitionViewModel?> GetByIdAsync(int id)
    {
        var competition = await _repo.GetByIdAsync(id);
        return competition == null ? null : MapToViewModel(competition);
    }

    public async Task CreateAsync(CompetitionViewModel vm)
    {
        var competition = new Competition
        {
            Name = vm.Name,
            Date = vm.Date,
            Location = vm.Location,
            CategoryId = vm.CategoryId
        };
        await _repo.AddAsync(competition);
    }

    public async Task UpdateAsync(CompetitionViewModel vm)
    {
        var competition = await _repo.GetByIdAsync(vm.Id);
        if (competition == null) return;
        competition.Name = vm.Name;
        competition.Date = vm.Date;
        competition.Location = vm.Location;
        competition.CategoryId = vm.CategoryId;
        await _repo.UpdateAsync(competition);
    }

    public async Task DeleteAsync(int id) => await _repo.DeleteAsync(id);

    private static CompetitionViewModel MapToViewModel(Competition c) => new()
    {
        Id = c.Id,
        Name = c.Name,
        Date = c.Date,
        Location = c.Location,
        CategoryId = c.CategoryId,
        CategoryName = c.Category?.Name,
        HasBracket = c.Brackets.Any(),
        BracketId = c.Brackets.FirstOrDefault()?.Id ?? 0
    };
}
