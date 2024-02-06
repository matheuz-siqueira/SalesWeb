using Microsoft.EntityFrameworkCore;
using SalesWeb.Mvc.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<SalesWebContext>(options => 
{
    options.UseMySql(
        builder.Configuration.GetConnectionString("Default"), 
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("Default")), 
        b => b.MigrationsAssembly(typeof(SalesWebContext).Assembly.FullName)
    );
});

builder.Services.AddScoped<SeedingService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

if(app.Environment.IsDevelopment())
    Seed(app); 

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

static void Seed(IApplicationBuilder app)
{
    using var serviceScope = app.ApplicationServices.CreateAsyncScope();
    var seed = serviceScope.ServiceProvider
        .GetService<SeedingService>(); 

    seed.Seed();
}