using System.ComponentModel.DataAnnotations;

namespace Versus.ViewModels;

public class CategoryViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Name is required")]
    [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
    public string Name { get; set; } = string.Empty;

    [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
    public string? Description { get; set; }

    [Display(Name = "Min Weight (kg)")]
    public double? MinWeight { get; set; }

    [Display(Name = "Max Weight (kg)")]
    public double? MaxWeight { get; set; }

    [Display(Name = "Min Age")]
    public int? MinAge { get; set; }

    [Display(Name = "Max Age")]
    public int? MaxAge { get; set; }

    public int CompetitorCount { get; set; }
}
