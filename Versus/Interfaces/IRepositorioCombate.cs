using Versus.Models;

namespace Versus.Interfaces;

public interface IRepositorioCombate
{
    Task<IEnumerable<Combate>> ObtenerPorLlaveAsync(int llaveId);
    Task<Combate?> ObtenerPorIdAsync(int id);
    Task AgregarRangoAsync(IEnumerable<Combate> combates);
    Task ActualizarAsync(Combate combate);
    Task EliminarPorLlaveAsync(int llaveId);
}
