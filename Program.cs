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

    string[] roles = { "Administrador", "Empleado" };

    foreach (var rol in roles)
    {
        if (!await roleManager.RoleExistsAsync(rol))
        {
            await roleManager.CreateAsync(new IdentityRole(rol));
        }
    }

    string adminEmail = "admin@tienda.com";

    var admin = await userManager.FindByEmailAsync(adminEmail);

    if (admin == null)
    {
        admin = new ApplicationUser
        {
            UserName = adminEmail,
            Email = adminEmail,
            EmailConfirmed = true
        };

        await userManager.CreateAsync(admin, "Admin123*");
        await userManager.AddToRoleAsync(admin, "Administrador");
    }

    string empleadoEmail = "empleado@tienda.com";

    var empleado = await userManager.FindByEmailAsync(empleadoEmail);

    if (empleado == null)
    {
        empleado = new ApplicationUser
        {
            UserName = empleadoEmail,
            Email = empleadoEmail,
            EmailConfirmed = true
        };

        await userManager.CreateAsync(empleado, "Empleado123*");
        await userManager.AddToRoleAsync(empleado, "Empleado");
    }
}

app.Run();
