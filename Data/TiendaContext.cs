using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Tienda.Models;

public class TiendaContext(DbContextOptions<TiendaContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Producto> Producto { get; set; } = default!;
    public DbSet<Categoria> Categoria { get; set; } = default!;
}