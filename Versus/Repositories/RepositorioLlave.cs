using Microsoft.EntityFrameworkCore;
using Versus.Data;
using Versus.Interfaces;
using Versus.Models;

namespace Versus.Repositories;

public class RepositorioLlave : IRepositorioLlave
{
    private readonly ApplicationDbContext _contexto;

    public RepositorioLlave(ApplicationDbContext contexto)
    {
        _contexto = contexto;
    }

    public async Task<Llave?> ObtenerPorIdAsync(int id)
        => await _contexto.Llaves
            .Include(l => l.Combates)
                .ThenInclude(c => c.Competidor1)
            .Include(l => l.Combates)
                .ThenInclude(c => c.Competidor2)
            .Include(l => l.Combates)
                .ThenInclude(c => c.Ganador)
            .FirstOrDefaultAsync(l => l.Id == id);

    public async Task<Llave?> ObtenerPorCompetenciaAsync(int competenciaId)
        => await _contexto.Llaves
            .Include(l => l.Combates)
                .ThenInclude(c => c.Competidor1)
            .Include(l => l.Combates)
                .ThenInclude(c => c.Competidor2)
            .Include(l => l.Combates)
                .ThenInclude(c => c.Ganador)
            .FirstOrDefaultAsync(l => l.CompetenciaId == competenciaId);

    public async Task AgregarAsync(Llave llave)
    {
        _contexto.Llaves.Add(llave);
        await _contexto.SaveChangesAsync();
    }

    public async Task ActualizarAsync(Llave llave)
    {
        _contexto.Llaves.Update(llave);
        await _contexto.SaveChangesAsync();
    }

    public async Task EliminarAsync(int id)
    {
        var llave = await _contexto.Llaves.FindAsync(id);
        if (llave != null)
        {
            _contexto.Llaves.Remove(llave);
            await _contexto.SaveChangesAsync();
        }
    }
}
