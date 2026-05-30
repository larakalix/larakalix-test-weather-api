using car.Api.Data;
using car.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace car.Api.Repositories;

public sealed class MarcasAutosRepository : IMarcasAutosRepository
{
    private readonly ApplicationDbContext _dbContext;

    // Se inyecta el contexto de la BD en el repositorio,
    // para poder acceder a los datos de la instancia.
    public MarcasAutosRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    // Obtenemos una lista de la tabla `MarcaAuto` de la BD,
    // mediante el contexto ordenados por ID.
    public async Task<IReadOnlyList<MarcaAuto>> GetAllAsync(
        CancellationToken cancellationToken = default
    )
    {
        return await _dbContext
            .MarcasAutos.AsNoTracking()
            .OrderBy(marca => marca.Id)
            .ToListAsync(cancellationToken);
    }
}
