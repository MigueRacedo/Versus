using Versus.Interfaces;
using Versus.Models;
using Versus.ViewModels;

namespace Versus.Services;

public class ServicioLlave : IServicioLlave
{
    private readonly IRepositorioLlave _repositorioLlave;
    private readonly IRepositorioCombate _repositorioCombate;
    private readonly IRepositorioCompetidor _repositorioCompetidor;
    private readonly IRepositorioCompetencia _repositorioCompetencia;

    public ServicioLlave(
        IRepositorioLlave repositorioLlave,
        IRepositorioCombate repositorioCombate,
        IRepositorioCompetidor repositorioCompetidor,
        IRepositorioCompetencia repositorioCompetencia)
    {
        _repositorioLlave = repositorioLlave;
        _repositorioCombate = repositorioCombate;
        _repositorioCompetidor = repositorioCompetidor;
        _repositorioCompetencia = repositorioCompetencia;
    }

    public async Task<LlaveViewModel?> ObtenerPorCompetenciaAsync(int competenciaId)
    {
        var llave = await _repositorioLlave.ObtenerPorCompetenciaAsync(competenciaId);
        if (llave == null) return null;
        return MapearAViewModel(llave);
    }

    public async Task<LlaveViewModel> GenerarAsync(int competenciaId)
    {
        var competencia = await _repositorioCompetencia.ObtenerPorIdAsync(competenciaId)
            ?? throw new InvalidOperationException("Competencia no encontrada.");

        var existente = await _repositorioLlave.ObtenerPorCompetenciaAsync(competenciaId);
        if (existente != null)
        {
            await _repositorioCombate.EliminarPorLlaveAsync(existente.Id);
            await _repositorioLlave.EliminarAsync(existente.Id);
        }

        var competidores = (await _repositorioCompetidor.ObtenerPorCategoriaAsync(competencia.CategoriaId)).ToList();
        if (competidores.Count < 2)
            throw new InvalidOperationException("Se necesitan al menos 2 competidores en la categoría para generar la llave.");

        var rng = new Random();
        competidores = competidores.OrderBy(_ => rng.Next()).ToList();

        var llave = new Llave
        {
            CompetenciaId = competenciaId,
            Nombre = $"{competencia.Nombre} - Llave"
        };
        await _repositorioLlave.AgregarAsync(llave);

        var combates = GenerarCombatesEliminacionSimple(llave.Id, competidores);
        await _repositorioCombate.AgregarRangoAsync(combates);

        var resultado = await _repositorioLlave.ObtenerPorIdAsync(llave.Id);
        return MapearAViewModel(resultado!);
    }

    public async Task<CombateViewModel?> ObtenerCombateAsync(int combateId)
    {
        var combate = await _repositorioCombate.ObtenerPorIdAsync(combateId);
        return combate == null ? null : MapearCombateAViewModel(combate);
    }

    public async Task RegistrarResultadoAsync(int combateId, int ganadorId)
    {
        var combate = await _repositorioCombate.ObtenerPorIdAsync(combateId)
            ?? throw new InvalidOperationException("Combate no encontrado.");

        if (combate.Competidor1Id != ganadorId && combate.Competidor2Id != ganadorId)
            throw new InvalidOperationException("El ganador debe ser uno de los competidores.");

        combate.GanadorId = ganadorId;
        combate.Estado = EstadoCombate.Completado;
        await _repositorioCombate.ActualizarAsync(combate);
    }

    private static List<Combate> GenerarCombatesEliminacionSimple(int llaveId, List<Competidor> competidores)
    {
        var combates = new List<Combate>();
        int tamanio = 1;
        while (tamanio < competidores.Count) tamanio *= 2;

        int numeroCombate = 1;
        var competidoresPrimeraRonda = new List<Competidor?>(competidores);
        while (competidoresPrimeraRonda.Count < tamanio) competidoresPrimeraRonda.Add(null);

        for (int i = 0; i < tamanio; i += 2)
        {
            var c1 = competidoresPrimeraRonda[i];
            var c2 = competidoresPrimeraRonda[i + 1];
            var estado = (c1 == null || c2 == null) ? EstadoCombate.Libre : EstadoCombate.Pendiente;
            combates.Add(new Combate
            {
                LlaveId = llaveId,
                Ronda = 1,
                NumeroCombate = numeroCombate++,
                Competidor1Id = c1?.Id,
                Competidor2Id = c2?.Id,
                Estado = estado
            });
        }

        int totalRondas = (int)Math.Log2(tamanio);
        for (int ronda = 2; ronda <= totalRondas; ronda++)
        {
            int combatesEnRonda = tamanio / (int)Math.Pow(2, ronda);
            for (int m = 0; m < combatesEnRonda; m++)
            {
                combates.Add(new Combate
                {
                    LlaveId = llaveId,
                    Ronda = ronda,
                    NumeroCombate = numeroCombate++,
                    Estado = EstadoCombate.Pendiente
                });
            }
        }

        return combates;
    }

    private static LlaveViewModel MapearAViewModel(Llave llave)
    {
        int totalRondas = llave.Combates.Any() ? llave.Combates.Max(c => c.Ronda) : 0;
        var rondas = llave.Combates
            .GroupBy(c => c.Ronda)
            .OrderBy(g => g.Key)
            .Select(g => new RondaViewModel
            {
                NumeroRonda = g.Key,
                NombreRonda = ObtenerNombreRonda(g.Key, totalRondas),
                Combates = g.OrderBy(c => c.NumeroCombate).Select(MapearCombateAViewModel).ToList()
            }).ToList();

        return new LlaveViewModel
        {
            Id = llave.Id,
            Nombre = llave.Nombre,
            CompetenciaId = llave.CompetenciaId,
            NombreCompetencia = llave.Competencia?.Nombre ?? string.Empty,
            Rondas = rondas
        };
    }

    private static string ObtenerNombreRonda(int ronda, int totalRondas)
    {
        int diferencia = totalRondas - ronda;
        return diferencia switch
        {
            0 => "Final",
            1 => "Semifinal",
            2 => "Cuartos de Final",
            _ => $"Ronda {ronda}"
        };
    }

    private static CombateViewModel MapearCombateAViewModel(Combate c) => new()
    {
        Id = c.Id,
        Ronda = c.Ronda,
        NumeroCombate = c.NumeroCombate,
        Competidor1Id = c.Competidor1Id,
        NombreCompetidor1 = c.Competidor1?.Nombre ?? "Por definir",
        Competidor2Id = c.Competidor2Id,
        NombreCompetidor2 = c.Competidor2?.Nombre ?? "Por definir",
        GanadorId = c.GanadorId,
        NombreGanador = c.Ganador?.Nombre,
        Estado = c.Estado.ToString(),
        Notas = c.Notas
    };
}
