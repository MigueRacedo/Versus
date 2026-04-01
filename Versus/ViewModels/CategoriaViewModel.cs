using System.ComponentModel.DataAnnotations;

namespace Versus.ViewModels;

public class CategoriaViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio")]
    [StringLength(100, ErrorMessage = "El nombre no puede superar los 100 caracteres")]
    [Display(Name = "Nombre")]
    public string Nombre { get; set; } = string.Empty;

    [StringLength(500, ErrorMessage = "La descripción no puede superar los 500 caracteres")]
    [Display(Name = "Descripción")]
    public string? Descripcion { get; set; }

    [Display(Name = "Peso Mínimo (kg)")]
    public double? PesoMinimo { get; set; }

    [Display(Name = "Peso Máximo (kg)")]
    public double? PesoMaximo { get; set; }

    [Display(Name = "Edad Mínima")]
    public int? EdadMinima { get; set; }

    [Display(Name = "Edad Máxima")]
    public int? EdadMaxima { get; set; }

    public int CantidadCompetidores { get; set; }
}
