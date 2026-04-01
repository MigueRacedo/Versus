using Microsoft.EntityFrameworkCore;
using Versus.Data;
using Versus.Interfaces;
using Versus.Models;

namespace Versus.Repositories;

public class RepositorioCompetidor : IRepositorioCompetidor
{
    private readonly ApplicationDbContext _contexto;

    public RepositorioCompetidor(ApplicationDbContext contexto)
    {
        _contexto = contexto;
    }

    public async Task<IEnumerable<Competidor>> ObtenerTodosAsync()
        => await _contexto.Competidores.Include(c => c.Categoria).ToListAsync();

    public async Task<Competidor?> ObtenerPorIdAsync(int id)
        => await _contexto.Competidores.Include(c => c.Categoria).FirstOrDefaultAsync(c => c.Id == id);

    public async Task<IEnumerable<Competidor>> ObtenerPorCategoriaAsync(int categoriaId)
        => await _contexto.Competidores.Include(c => c.Categoria).Where(c => c.CategoriaId == categoriaId).ToListAsync();

    public async Task AgregarAsync(Competidor competidor)
    {
        _contexto.Competidores.Add(competidor);
        await _contexto.SaveChangesAsync();
    }

    public async Task ActualizarAsync(Competidor competidor)
    {
        _contexto.Competidores.Update(competidor);
        await _contexto.SaveChangesAsync();
    }

    public async Task EliminarAsync(int id)
    {
        var competidor = await _contexto.Competidores.FindAsync(id);
        if (competidor != null)
        {
            _contexto.Competidores.Remove(competidor);
            await _contexto.SaveChangesAsync();
        }
    }

    public async Task<bool> ExisteAsync(int id)
        => await _contexto.Competidores.AnyAsync(c => c.Id == id);
}
