using System.ComponentModel.DataAnnotations;

namespace Versus.ViewModels;

public class CompetitionViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Name is required")]
    [StringLength(200, ErrorMessage = "Name cannot exceed 200 characters")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Date is required")]
    [DataType(DataType.Date)]
    public DateTime Date { get; set; } = DateTime.Today;

    [StringLength(200)]
    public string? Location { get; set; }

    [Required(ErrorMessage = "Category is required")]
    [Display(Name = "Category")]
    public int CategoryId { get; set; }
    public string? CategoryName { get; set; }

    public bool HasBracket { get; set; }
    public int BracketId { get; set; }
}
