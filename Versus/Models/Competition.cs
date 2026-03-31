using System.ComponentModel.DataAnnotations;

namespace Versus.Models;

public class Competition
{
    public int Id { get; set; }

    [Required, StringLength(200)]
    public string Name { get; set; } = string.Empty;

    [Required]
    public DateTime Date { get; set; }

    [StringLength(200)]
    public string? Location { get; set; }

    public int CategoryId { get; set; }
    public Category? Category { get; set; }

    public ICollection<Bracket> Brackets { get; set; } = new List<Bracket>();
}
