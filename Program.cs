using Microsoft.EntityFrameworkCore;
using Tienda.Models;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("TiendaContext") ?? throw new InvalidOperationException("Connection string 'TiendaContext' not found.");

builder.Services.AddDbContext<TiendaContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
    options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddErrorDescriber<IdentityErrorDescriberEs>()
    .AddEntityFrameworkStores<TiendaContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapRazorPages();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

    string[] roles = { "Administrador", "Cliente" };

    foreach (var rol in roles)
    {
        if (!await roleManager.RoleExistsAsync(rol))
        {
            await roleManager.CreateAsync(new IdentityRole(rol));
        }
    }

    await CrearUsuarioConRolAsync(userManager, "admin@tienda.com", "Admin123*", "Administrador");
    await CrearUsuarioConRolAsync(userManager, "cliente@tienda.com", "Cliente123*", "Cliente");
}

app.Run();

static async Task CrearUsuarioConRolAsync(UserManager<ApplicationUser> userManager, string correo, string contrasena, string rol)
{
    var usuario = await userManager.FindByEmailAsync(correo);

    if (usuario == null)
    {
        usuario = new ApplicationUser
        {
            UserName = correo,
            Email = correo,
            EmailConfirmed = true
        };

        await userManager.CreateAsync(usuario, contrasena);
    }

    // El rol se asigna fuera del bloque que crea el usuario
    if (!await userManager.IsInRoleAsync(usuario, rol))
    {
        await userManager.AddToRoleAsync(usuario, rol);
    }
}