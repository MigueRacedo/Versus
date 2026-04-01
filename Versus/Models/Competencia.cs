using System.ComponentModel.DataAnnotations;

namespace Versus.Models;

public class Competencia
{
    public int Id { get; set; }

    [Required, StringLength(200)]
    public string Nombre { get; set; } = string.Empty;

    [Required]
    public DateTime Fecha { get; set; }

    [StringLength(200)]
    public string? Lugar { get; set; }

    public int CategoriaId { get; set; }
    public Categoria? Categoria { get; set; }

    public ICollection<Llave> Llaves { get; set; } = new List<Llave>();
}
