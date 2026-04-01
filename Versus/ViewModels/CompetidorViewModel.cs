using System.ComponentModel.DataAnnotations;

namespace Versus.ViewModels;

public class CompetidorViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio")]
    [StringLength(100, ErrorMessage = "El nombre no puede superar los 100 caracteres")]
    [Display(Name = "Nombre Completo")]
    public string Nombre { get; set; } = string.Empty;

    [Required(ErrorMessage = "La edad es obligatoria")]
    [Range(1, 150, ErrorMessage = "La edad debe estar entre 1 y 150")]
    [Display(Name = "Edad")]
    public int Edad { get; set; }

    [Required(ErrorMessage = "El peso es obligatorio")]
    [Range(0.1, 500.0, ErrorMessage = "El peso debe estar entre 0.1 y 500 kg")]
    [Display(Name = "Peso (kg)")]
    public double Peso { get; set; }

    [Display(Name = "Categoría")]
    public int? CategoriaId { get; set; }
    public string? NombreCategoria { get; set; }
}
