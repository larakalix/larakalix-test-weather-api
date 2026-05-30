using car.Api.Data;
using car.Api.DTOs;
using car.Api.Repositories;
using Microsoft.EntityFrameworkCore;

namespace car.Api.Services;

public sealed class MarcasAutosService : IMarcasAutosService
{
    private readonly IMarcasAutosRepository _repository;

    // Se inyecta el repositorio en el controlador,
    // para poder acceder a los metodos del repositorio como dependencia.
    public MarcasAutosService(IMarcasAutosRepository repository)
    {
        _repository = repository;
    }

    // En este metodo, obtenemos una lista de la marca de autos, desde el repositorio, desde esta instancia no accedemos a la
    // base de datos directamente, sino que lo hacemos referenciando al repositorio,
    // luego convertimos cada marca a un DTO que es devuelto a quien haga referencia a este servicio.
    public async Task<IReadOnlyList<MarcaAutoDto>> GetAllAsync(
        CancellationToken cancellationToken = default
    )
    {
        var marcas = await _repository.GetAllAsync(cancellationToken);

        return marcas
            .Select(marca => new MarcaAutoDto
            {
                Id = marca.Id,
                Nombre = marca.Nombre,
                Pais = marca.Pais,
            })
            .ToList();
    }
}
