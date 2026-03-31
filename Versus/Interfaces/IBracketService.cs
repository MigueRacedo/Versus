using Versus.ViewModels;

namespace Versus.Interfaces;

public interface IBracketService
{
    Task<BracketViewModel?> GetByCompetitionAsync(int competitionId);
    Task<BracketViewModel> GenerateAsync(int competitionId);
    Task<MatchViewModel?> GetMatchAsync(int matchId);
    Task RecordResultAsync(int matchId, int winnerId);
}
