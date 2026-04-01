using System.ComponentModel.DataAnnotations;

namespace Versus.Models;

public class Llave
{
    public int Id { get; set; }

    public int CompetenciaId { get; set; }
    public Competencia? Competencia { get; set; }

    [Required, StringLength(100)]
    public string Nombre { get; set; } = string.Empty;

    public ICollection<Combate> Combates { get; set; } = new List<Combate>();
}
