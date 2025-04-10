using Microsoft.EntityFrameworkCore;
using ScreenSoundMVC.Data;
using ScreenSoundMVC.Models.Identity;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddRazorPages();

// Configuração do banco de dados
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? 
    throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

// Configuração do DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
           .UseLazyLoadingProxies());

// Configuração do Identity (apenas uma vez)
builder.Services.AddIdentity<PessoaComAcesso, PerfilDeAcesso>(options => 
    {
        options.SignIn.RequireConfirmedAccount = false;
        options.Password.RequireDigit = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireUppercase = true;
        options.Password.RequireNonAlphanumeric = true;
        options.Password.RequiredLength = 8;
    })
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();


// Código para criar usuário e perfil padrão

using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<PessoaComAcesso>>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<PerfilDeAcesso>>();
    
    // Criar perfil Admin se não existir
    if (!roleManager.RoleExistsAsync("Admin").Result)
    {
        var role = new PerfilDeAcesso { Name = "Admin" };
        roleManager.CreateAsync(role).Wait();
    }
    
    // Criar usuário admin se não existir
    var admin = userManager.FindByEmailAsync("admin@admin.com").Result;
    if (admin == null)
    {
        admin = new PessoaComAcesso
        {
            UserName = "admin@admin.com",
            Email = "admin@admin.com",
            EmailConfirmed = true
        };
        
        var result = userManager.CreateAsync(admin, "Admin@123").Result;
        if (result.Succeeded)
        {
            userManager.AddToRoleAsync(admin, "Admin").Wait();
        }
    }
}

app.Run();