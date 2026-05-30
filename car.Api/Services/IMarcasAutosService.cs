using car.Api.DTOs;

namespace car.Api.Services;

public interface IMarcasAutosService
{
    Task<IReadOnlyList<MarcaAutoDto>> GetAllAsync(CancellationToken cancellationToken = default);
}
