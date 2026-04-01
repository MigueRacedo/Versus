using Versus.Models;

namespace Versus.Interfaces;

public interface IRepositorioLlave
{
    Task<Llave?> ObtenerPorIdAsync(int id);
    Task<Llave?> ObtenerPorCompetenciaAsync(int competenciaId);
    Task AgregarAsync(Llave llave);
    Task ActualizarAsync(Llave llave);
    Task EliminarAsync(int id);
}
