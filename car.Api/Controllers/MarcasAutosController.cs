using car.Api.DTOs;
using car.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace car.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class MarcasAutosController : ControllerBase
{
    private readonly IMarcasAutosService _marcasAutosService;

    // Se inyecta el servicio en el controlador,
    // para poder acceder a los metodos del servicio como dependencia.
    public MarcasAutosController(IMarcasAutosService marcasAutosService) => _marcasAutosService = marcasAutosService;

    // En este endpoint, obtenemos una lista de las marcas de autos,
    // utilizand el servicio, y devolvermos el resultado del metodo,
    // desde este punto, el endpoint obtiene la informacion sin acceder
    // directamente a la BD.
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<MarcaAutoDto>>> GetAll(
        CancellationToken cancellationToken
    )
    {
        var data = await _marcasAutosService.GetAllAsync(cancellationToken);

        return Ok(data);
    }
}
