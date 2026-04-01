using System.ComponentModel.DataAnnotations;

namespace Versus.ViewModels;

public class CompetenciaViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio")]
    [StringLength(200, ErrorMessage = "El nombre no puede superar los 200 caracteres")]
    [Display(Name = "Nombre")]
    public string Nombre { get; set; } = string.Empty;

    [Required(ErrorMessage = "La fecha es obligatoria")]
    [DataType(DataType.Date)]
    [Display(Name = "Fecha")]
    public DateTime Fecha { get; set; } = DateTime.Today;

    [StringLength(200)]
    [Display(Name = "Lugar")]
    public string? Lugar { get; set; }

    [Required(ErrorMessage = "La categoría es obligatoria")]
    [Display(Name = "Categoría")]
    public int CategoriaId { get; set; }
    public string? NombreCategoria { get; set; }

    public bool TieneLlave { get; set; }
    public int LlaveId { get; set; }
}
