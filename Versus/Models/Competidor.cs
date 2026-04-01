using System.ComponentModel.DataAnnotations;

namespace Versus.Models;

public class Competidor
{
    public int Id { get; set; }

    [Required, StringLength(100)]
    public string Nombre { get; set; } = string.Empty;

    [Range(1, 150)]
    public int Edad { get; set; }

    [Range(0.1, 500.0)]
    public double Peso { get; set; }

    public int? CategoriaId { get; set; }
    public Categoria? Categoria { get; set; }

    public ICollection<Combate> CombatesComoCompetidor1 { get; set; } = new List<Combate>();
    public ICollection<Combate> CombatesComoCompetidor2 { get; set; } = new List<Combate>();
}
