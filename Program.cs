using IntraNet.Data;
using IntraNet.Hubs;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// MVC
builder.Services.AddControllersWithViews();

// SIGNALR
builder.Services.AddSignalR();

// BANCO DE DADOS
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

// IDENTITY
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
})
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<ApplicationDbContext>();

var app = builder.Build();

// ================= PIPELINE =================

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// ================= ROTAS =================

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

// SIGNALR HUB
app.MapHub<ChatHub>("/chatHub");

// ================= SEED ADMIN =================

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await SeedData.CreateAdminAsync(services);
}

// ================= START APP =================

app.Run();
