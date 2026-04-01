using System.ComponentModel.DataAnnotations;

namespace Versus.Models;

public class Combate
{
    public int Id { get; set; }

    public int LlaveId { get; set; }
    public Llave? Llave { get; set; }

    public int? Competidor1Id { get; set; }
    public Competidor? Competidor1 { get; set; }

    public int? Competidor2Id { get; set; }
    public Competidor? Competidor2 { get; set; }

    public int? GanadorId { get; set; }
    public Competidor? Ganador { get; set; }

    public int Ronda { get; set; }
    public int NumeroCombate { get; set; }

    public EstadoCombate Estado { get; set; } = EstadoCombate.Pendiente;

    [StringLength(500)]
    public string? Notas { get; set; }
}

public enum EstadoCombate
{
    Pendiente,
    EnProgreso,
    Completado,
    Libre
}
