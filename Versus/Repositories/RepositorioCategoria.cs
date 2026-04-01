using Microsoft.EntityFrameworkCore;
using Versus.Data;
using Versus.Interfaces;
using Versus.Models;

namespace Versus.Repositories;

public class RepositorioCategoria : IRepositorioCategoria
{
    private readonly ApplicationDbContext _contexto;

    public RepositorioCategoria(ApplicationDbContext contexto)
    {
        _contexto = contexto;
    }

    public async Task<IEnumerable<Categoria>> ObtenerTodosAsync()
        => await _contexto.Categorias.Include(c => c.Competidores).ToListAsync();

    public async Task<Categoria?> ObtenerPorIdAsync(int id)
        => await _contexto.Categorias.Include(c => c.Competidores).FirstOrDefaultAsync(c => c.Id == id);

    public async Task AgregarAsync(Categoria categoria)
    {
        _contexto.Categorias.Add(categoria);
        await _contexto.SaveChangesAsync();
    }

    public async Task ActualizarAsync(Categoria categoria)
    {
        _contexto.Categorias.Update(categoria);
        await _contexto.SaveChangesAsync();
    }

    public async Task EliminarAsync(int id)
    {
        var categoria = await _contexto.Categorias.FindAsync(id);
        if (categoria != null)
        {
            _contexto.Categorias.Remove(categoria);
            await _contexto.SaveChangesAsync();
        }
    }

    public async Task<bool> ExisteAsync(int id)
        => await _contexto.Categorias.AnyAsync(c => c.Id == id);
}
