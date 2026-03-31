using System.ComponentModel.DataAnnotations;

namespace Versus.Models;

public class Competitor
{
    public int Id { get; set; }

    [Required, StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [Range(1, 150)]
    public int Age { get; set; }

    [Range(0.1, 500.0)]
    public double Weight { get; set; }

    public int? CategoryId { get; set; }
    public Category? Category { get; set; }

    public ICollection<Match> MatchesAsCompetitor1 { get; set; } = new List<Match>();
    public ICollection<Match> MatchesAsCompetitor2 { get; set; } = new List<Match>();
}
