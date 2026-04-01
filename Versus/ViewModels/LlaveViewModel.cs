namespace Versus.ViewModels;

public class LlaveViewModel
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public int CompetenciaId { get; set; }
    public string NombreCompetencia { get; set; } = string.Empty;
    public List<RondaViewModel> Rondas { get; set; } = new();
}

public class RondaViewModel
{
    public int NumeroRonda { get; set; }
    public string NombreRonda { get; set; } = string.Empty;
    public List<CombateViewModel> Combates { get; set; } = new();
}

public class CombateViewModel
{
    public int Id { get; set; }
    public int Ronda { get; set; }
    public int NumeroCombate { get; set; }
    public string? NombreCompetidor1 { get; set; }
    public int? Competidor1Id { get; set; }
    public string? NombreCompetidor2 { get; set; }
    public int? Competidor2Id { get; set; }
    public string? NombreGanador { get; set; }
    public int? GanadorId { get; set; }
    public string Estado { get; set; } = "Pendiente";
    public string? Notas { get; set; }
}
