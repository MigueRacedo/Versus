using System.ComponentModel.DataAnnotations;

namespace Versus.Models;

public class Categoria
{
    public int Id { get; set; }

    [Required, StringLength(100)]
    public string Nombre { get; set; } = string.Empty;

    [StringLength(500)]
    public string? Descripcion { get; set; }

    public double? PesoMinimo { get; set; }
    public double? PesoMaximo { get; set; }
    public int? EdadMinima { get; set; }
    public int? EdadMaxima { get; set; }

    public ICollection<Competidor> Competidores { get; set; } = new List<Competidor>();
    public ICollection<Competencia> Competencias { get; set; } = new List<Competencia>();
}
