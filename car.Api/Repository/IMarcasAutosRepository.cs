using car.Api.Models;

namespace car.Api.Repositories;

public interface IMarcasAutosRepository
{
    Task<IReadOnlyList<MarcaAuto>> GetAllAsync(CancellationToken cancellationToken = default);
}
