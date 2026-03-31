using System.ComponentModel.DataAnnotations;

namespace Versus.ViewModels;

public class CompetitorViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Name is required")]
    [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
    [Display(Name = "Full Name")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Age is required")]
    [Range(1, 150, ErrorMessage = "Age must be between 1 and 150")]
    public int Age { get; set; }

    [Required(ErrorMessage = "Weight is required")]
    [Range(0.1, 500.0, ErrorMessage = "Weight must be between 0.1 and 500 kg")]
    [Display(Name = "Weight (kg)")]
    public double Weight { get; set; }

    [Display(Name = "Category")]
    public int? CategoryId { get; set; }
    public string? CategoryName { get; set; }
}
