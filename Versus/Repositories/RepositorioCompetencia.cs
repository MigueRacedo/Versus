using Microsoft.EntityFrameworkCore;
using Versus.Data;
using Versus.Interfaces;
using Versus.Models;

namespace Versus.Repositories;

public class RepositorioCompetencia : IRepositorioCompetencia
{
    private readonly ApplicationDbContext _contexto;

    public RepositorioCompetencia(ApplicationDbContext contexto)
    {
        _contexto = contexto;
    }

    public async Task<IEnumerable<Competencia>> ObtenerTodosAsync()
        => await _contexto.Competencias.Include(c => c.Categoria).Include(c => c.Llaves).ToListAsync();

    public async Task<Competencia?> ObtenerPorIdAsync(int id)
        => await _contexto.Competencias.Include(c => c.Categoria).Include(c => c.Llaves).FirstOrDefaultAsync(c => c.Id == id);

    public async Task AgregarAsync(Competencia competencia)
    {
        _contexto.Competencias.Add(competencia);
        await _contexto.SaveChangesAsync();
    }

    public async Task ActualizarAsync(Competencia competencia)
    {
        _contexto.Competencias.Update(competencia);
        await _contexto.SaveChangesAsync();
    }

    public async Task EliminarAsync(int id)
    {
        var competencia = await _contexto.Competencias.FindAsync(id);
        if (competencia != null)
        {
            _contexto.Competencias.Remove(competencia);
            await _contexto.SaveChangesAsync();
        }
    }

    public async Task<bool> ExisteAsync(int id)
        => await _contexto.Competencias.AnyAsync(c => c.Id == id);
}
