using Versus.ViewModels;

namespace Versus.Interfaces;

public interface IServicioLlave
{
    Task<LlaveViewModel?> ObtenerPorCompetenciaAsync(int competenciaId);
    Task<LlaveViewModel> GenerarAsync(int competenciaId);
    Task<CombateViewModel?> ObtenerCombateAsync(int combateId);
    Task RegistrarResultadoAsync(int combateId, int ganadorId);
}
