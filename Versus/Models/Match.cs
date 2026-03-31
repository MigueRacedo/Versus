using System.ComponentModel.DataAnnotations;

namespace Versus.Models;

public class Match
{
    public int Id { get; set; }

    public int BracketId { get; set; }
    public Bracket? Bracket { get; set; }

    public int? Competitor1Id { get; set; }
    public Competitor? Competitor1 { get; set; }

    public int? Competitor2Id { get; set; }
    public Competitor? Competitor2 { get; set; }

    public int? WinnerId { get; set; }
    public Competitor? Winner { get; set; }

    public int Round { get; set; }
    public int MatchNumber { get; set; }

    public MatchStatus Status { get; set; } = MatchStatus.Pending;

    [StringLength(500)]
    public string? Notes { get; set; }
}

public enum MatchStatus
{
    Pending,
    InProgress,
    Completed,
    Bye
}
