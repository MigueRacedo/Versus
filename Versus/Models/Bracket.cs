using System.ComponentModel.DataAnnotations;

namespace Versus.Models;

public class Bracket
{
    public int Id { get; set; }

    public int CompetitionId { get; set; }
    public Competition? Competition { get; set; }

    [Required, StringLength(100)]
    public string Name { get; set; } = string.Empty;

    public ICollection<Match> Matches { get; set; } = new List<Match>();
}
