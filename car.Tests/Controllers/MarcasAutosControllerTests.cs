using car.Api.Controllers;
using car.Api.Data;
using car.Api.DTOs;
using car.Api.Models;
using car.Api.Repositories;
using car.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace car.Tests.Controllers;

public class MarcasAutosControllerTests
{
    [Fact]
    public async Task GetAll_ShouldReturnOkWithExpectedCarBrands()
    {
        var dbContext = CreateDbContext();

        dbContext.MarcasAutos.AddRange(
            new MarcaAuto
            {
                Id = 1,
                Nombre = "Toyota",
                Pais = "Japan",
            },
            new MarcaAuto
            {
                Id = 2,
                Nombre = "Ford",
                Pais = "United States",
            },
            new MarcaAuto
            {
                Id = 3,
                Nombre = "BMW",
                Pais = "Germany",
            }
        );

        await dbContext.SaveChangesAsync();

        var repository = new MarcasAutosRepository(dbContext);
        var service = new MarcasAutosService(repository);
        var controller = new MarcasAutosController(service);

        var response = await controller.GetAll(CancellationToken.None);

        var okResult = Assert.IsType<OkObjectResult>(response.Result);
        var marcas = Assert.IsAssignableFrom<IReadOnlyList<MarcaAutoDto>>(okResult.Value);

        Assert.Equal(3, marcas.Count);

        Assert.Equal(1, marcas[0].Id);
        Assert.Equal("Toyota", marcas[0].Nombre);
        Assert.Equal("Japan", marcas[0].Pais);

        Assert.Equal(2, marcas[1].Id);
        Assert.Equal("Ford", marcas[1].Nombre);
        Assert.Equal("United States", marcas[1].Pais);

        Assert.Equal(3, marcas[2].Id);
        Assert.Equal("BMW", marcas[2].Nombre);
        Assert.Equal("Germany", marcas[2].Pais);
    }

    private static ApplicationDbContext CreateDbContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        return new ApplicationDbContext(options);
    }
}
