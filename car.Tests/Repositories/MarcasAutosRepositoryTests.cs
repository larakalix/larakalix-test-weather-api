using car.Api.Data;
using car.Api.Models;
using car.Api.Repositories;
using Microsoft.EntityFrameworkCore;

namespace car.Tests.Repositories;

public class MarcasAutosRepositoryTests
{
    [Fact]
    public async Task GetAllAsync_ShouldReturnAllCarBrandsOrderedById()
    {
        var dbContext = CreateDbContext();

        dbContext.MarcasAutos.AddRange(
            new MarcaAuto
            {
                Id = 3,
                Nombre = "BMW",
                Pais = "Germany",
            },
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
            }
        );

        await dbContext.SaveChangesAsync();

        var repository = new MarcasAutosRepository(dbContext);

        var result = await repository.GetAllAsync();

        Assert.NotNull(result);
        Assert.Equal(3, result.Count);

        Assert.Equal(1, result[0].Id);
        Assert.Equal("Toyota", result[0].Nombre);
        Assert.Equal("Japan", result[0].Pais);

        Assert.Equal(2, result[1].Id);
        Assert.Equal("Ford", result[1].Nombre);
        Assert.Equal("United States", result[1].Pais);

        Assert.Equal(3, result[2].Id);
        Assert.Equal("BMW", result[2].Nombre);
        Assert.Equal("Germany", result[2].Pais);
    }

    private static ApplicationDbContext CreateDbContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        return new ApplicationDbContext(options);
    }
}
