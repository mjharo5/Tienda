using Microsoft.EntityFrameworkCore;

public class TiendaContext(DbContextOptions<TiendaContext> options) : DbContext(options)
{
    public DbSet<Tienda.Models.Producto> Producto { get; set; } = default!;
    public DbSet<Tienda.Models.Categoria> Categoria { get; set; } = default!;
}
