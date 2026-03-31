using Versus.Interfaces;
using Versus.Models;
using Versus.ViewModels;

namespace Versus.Services;

public class BracketService : IBracketService
{
    private readonly IBracketRepository _bracketRepo;
    private readonly IMatchRepository _matchRepo;
    private readonly ICompetitorRepository _competitorRepo;
    private readonly ICompetitionRepository _competitionRepo;

    public BracketService(
        IBracketRepository bracketRepo,
        IMatchRepository matchRepo,
        ICompetitorRepository competitorRepo,
        ICompetitionRepository competitionRepo)
    {
        _bracketRepo = bracketRepo;
        _matchRepo = matchRepo;
        _competitorRepo = competitorRepo;
        _competitionRepo = competitionRepo;
    }

    public async Task<BracketViewModel?> GetByCompetitionAsync(int competitionId)
    {
        var bracket = await _bracketRepo.GetByCompetitionAsync(competitionId);
        if (bracket == null) return null;
        return MapToViewModel(bracket);
    }

    public async Task<BracketViewModel> GenerateAsync(int competitionId)
    {
        var competition = await _competitionRepo.GetByIdAsync(competitionId)
            ?? throw new InvalidOperationException("Competition not found.");

        var existing = await _bracketRepo.GetByCompetitionAsync(competitionId);
        if (existing != null)
        {
            await _matchRepo.DeleteByBracketAsync(existing.Id);
            await _bracketRepo.DeleteAsync(existing.Id);
        }

        var competitors = (await _competitorRepo.GetByCategoryAsync(competition.CategoryId)).ToList();
        if (competitors.Count < 2)
            throw new InvalidOperationException("At least 2 competitors in the category are required to generate a bracket.");

        var rng = new Random();
        competitors = competitors.OrderBy(_ => rng.Next()).ToList();

        var bracket = new Bracket
        {
            CompetitionId = competitionId,
            Name = $"{competition.Name} - Bracket"
        };
        await _bracketRepo.AddAsync(bracket);

        var matches = GenerateSingleEliminationMatches(bracket.Id, competitors);
        await _matchRepo.AddRangeAsync(matches);

        var result = await _bracketRepo.GetByIdAsync(bracket.Id);
        return MapToViewModel(result!);
    }

    public async Task<MatchViewModel?> GetMatchAsync(int matchId)
    {
        var match = await _matchRepo.GetByIdAsync(matchId);
        return match == null ? null : MapMatchToViewModel(match);
    }

    public async Task RecordResultAsync(int matchId, int winnerId)
    {
        var match = await _matchRepo.GetByIdAsync(matchId)
            ?? throw new InvalidOperationException("Match not found.");

        if (match.Competitor1Id != winnerId && match.Competitor2Id != winnerId)
            throw new InvalidOperationException("Winner must be one of the competitors.");

        match.WinnerId = winnerId;
        match.Status = MatchStatus.Completed;
        await _matchRepo.UpdateAsync(match);
    }

    private static List<Match> GenerateSingleEliminationMatches(int bracketId, List<Competitor> competitors)
    {
        var matches = new List<Match>();
        int size = 1;
        while (size < competitors.Count) size *= 2;

        int matchNumber = 1;
        var firstRoundCompetitors = new List<Competitor?>(competitors);
        while (firstRoundCompetitors.Count < size) firstRoundCompetitors.Add(null);

        for (int i = 0; i < size; i += 2)
        {
            var c1 = firstRoundCompetitors[i];
            var c2 = firstRoundCompetitors[i + 1];
            var status = (c1 == null || c2 == null) ? MatchStatus.Bye : MatchStatus.Pending;
            matches.Add(new Match
            {
                BracketId = bracketId,
                Round = 1,
                MatchNumber = matchNumber++,
                Competitor1Id = c1?.Id,
                Competitor2Id = c2?.Id,
                Status = status
            });
        }

        int totalRounds = (int)Math.Log2(size);
        for (int round = 2; round <= totalRounds; round++)
        {
            int matchesInRound = size / (int)Math.Pow(2, round);
            for (int m = 0; m < matchesInRound; m++)
            {
                matches.Add(new Match
                {
                    BracketId = bracketId,
                    Round = round,
                    MatchNumber = matchNumber++,
                    Status = MatchStatus.Pending
                });
            }
        }

        return matches;
    }

    private static BracketViewModel MapToViewModel(Bracket bracket)
    {
        int totalRounds = bracket.Matches.Any() ? bracket.Matches.Max(m => m.Round) : 0;
        var rounds = bracket.Matches
            .GroupBy(m => m.Round)
            .OrderBy(g => g.Key)
            .Select(g => new RoundViewModel
            {
                RoundNumber = g.Key,
                RoundName = GetRoundName(g.Key, totalRounds),
                Matches = g.OrderBy(m => m.MatchNumber).Select(MapMatchToViewModel).ToList()
            }).ToList();

        return new BracketViewModel
        {
            Id = bracket.Id,
            Name = bracket.Name,
            CompetitionId = bracket.CompetitionId,
            CompetitionName = bracket.Competition?.Name ?? string.Empty,
            Rounds = rounds
        };
    }

    private static string GetRoundName(int round, int totalRounds)
    {
        int diff = totalRounds - round;
        return diff switch
        {
            0 => "Final",
            1 => "Semi-Final",
            2 => "Quarter-Final",
            _ => $"Round {round}"
        };
    }

    private static MatchViewModel MapMatchToViewModel(Match m) => new()
    {
        Id = m.Id,
        Round = m.Round,
        MatchNumber = m.MatchNumber,
        Competitor1Id = m.Competitor1Id,
        Competitor1Name = m.Competitor1?.Name ?? "TBD",
        Competitor2Id = m.Competitor2Id,
        Competitor2Name = m.Competitor2?.Name ?? "TBD",
        WinnerId = m.WinnerId,
        WinnerName = m.Winner?.Name,
        Status = m.Status.ToString(),
        Notes = m.Notes
    };
}
