namespace Versus.ViewModels;

public class BracketViewModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int CompetitionId { get; set; }
    public string CompetitionName { get; set; } = string.Empty;
    public List<RoundViewModel> Rounds { get; set; } = new();
}

public class RoundViewModel
{
    public int RoundNumber { get; set; }
    public string RoundName { get; set; } = string.Empty;
    public List<MatchViewModel> Matches { get; set; } = new();
}

public class MatchViewModel
{
    public int Id { get; set; }
    public int Round { get; set; }
    public int MatchNumber { get; set; }
    public string? Competitor1Name { get; set; }
    public int? Competitor1Id { get; set; }
    public string? Competitor2Name { get; set; }
    public int? Competitor2Id { get; set; }
    public string? WinnerName { get; set; }
    public int? WinnerId { get; set; }
    public string Status { get; set; } = "Pending";
    public string? Notes { get; set; }
}
