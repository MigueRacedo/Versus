using System.ComponentModel.DataAnnotations;

namespace Versus.Models;

public class Category
{
    public int Id { get; set; }

    [Required, StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [StringLength(500)]
    public string? Description { get; set; }

    public double? MinWeight { get; set; }
    public double? MaxWeight { get; set; }
    public int? MinAge { get; set; }
    public int? MaxAge { get; set; }

    public ICollection<Competitor> Competitors { get; set; } = new List<Competitor>();
    public ICollection<Competition> Competitions { get; set; } = new List<Competition>();
}
