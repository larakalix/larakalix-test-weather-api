using car.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace car.Api.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    // Definimos un DbSet para manejar la entidad `MarcaAuto` en la base de datos,
    // para poder manpular los datos utilizando el contexto.
    public DbSet<MarcaAuto> MarcasAutos => Set<MarcaAuto>();

    // Configuramos la entidad `MarcaAuto` utilizando Fluent API,
    // para definir las propiedades y validaciones, y de igual manera
    // poblar la tabla con data inicial.
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<MarcaAuto>(entity =>
        {
            entity.ToTable("MarcasAutos");

            entity.HasKey(marca => marca.Id);

            entity.Property(marca => marca.Nombre).IsRequired().HasMaxLength(100);

            entity.Property(marca => marca.Pais).IsRequired().HasMaxLength(100);

            entity.HasData(
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
                },
                new MarcaAuto
                {
                    Id = 4,
                    Nombre = "Mercedes-Benz",
                    Pais = "Germany",
                },
                new MarcaAuto
                {
                    Id = 5,
                    Nombre = "Honda",
                    Pais = "Japan",
                },
                new MarcaAuto
                {
                    Id = 6,
                    Nombre = "Nissan",
                    Pais = "Japan",
                },
                new MarcaAuto
                {
                    Id = 7,
                    Nombre = "Chevrolet",
                    Pais = "United States",
                },
                new MarcaAuto
                {
                    Id = 8,
                    Nombre = "Hyundai",
                    Pais = "South Korea",
                },
                new MarcaAuto
                {
                    Id = 9,
                    Nombre = "Kia",
                    Pais = "South Korea",
                },
                new MarcaAuto
                {
                    Id = 10,
                    Nombre = "Volkswagen",
                    Pais = "Germany",
                }
            );
        });
    }
}
