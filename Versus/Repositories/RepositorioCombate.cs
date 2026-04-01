using Microsoft.EntityFrameworkCore;
using Versus.Data;
using Versus.Interfaces;
using Versus.Models;

namespace Versus.Repositories;

public class RepositorioCombate : IRepositorioCombate
{
    private readonly ApplicationDbContext _contexto;

    public RepositorioCombate(ApplicationDbContext contexto)
    {
        _contexto = contexto;
    }

    public async Task<IEnumerable<Combate>> ObtenerPorLlaveAsync(int llaveId)
        => await _contexto.Combates
            .Include(c => c.Competidor1)
            .Include(c => c.Competidor2)
            .Include(c => c.Ganador)
            .Where(c => c.LlaveId == llaveId)
            .OrderBy(c => c.Ronda).ThenBy(c => c.NumeroCombate)
            .ToListAsync();

    public async Task<Combate?> ObtenerPorIdAsync(int id)
        => await _contexto.Combates
            .Include(c => c.Competidor1)
            .Include(c => c.Competidor2)
            .Include(c => c.Ganador)
            .FirstOrDefaultAsync(c => c.Id == id);

    public async Task AgregarRangoAsync(IEnumerable<Combate> combates)
    {
        _contexto.Combates.AddRange(combates);
        await _contexto.SaveChangesAsync();
    }

    public async Task ActualizarAsync(Combate combate)
    {
        _contexto.Combates.Update(combate);
        await _contexto.SaveChangesAsync();
    }

    public async Task EliminarPorLlaveAsync(int llaveId)
    {
        var combates = await _contexto.Combates.Where(c => c.LlaveId == llaveId).ToListAsync();
        _contexto.Combates.RemoveRange(combates);
        await _contexto.SaveChangesAsync();
    }
}
