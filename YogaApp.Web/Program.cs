using System.Data;
using MySql.Data.MySqlClient;
using YogaApp.Application.Interfaces;
using YogaApp.Application.UseCases;
using YogaApp.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IDbConnection>(s => 
{
    var connectionString = builder.Configuration.GetConnectionString("yogaApi");
    return new MySqlConnection(connectionString);
});

builder.Services.AddScoped<IPoseRepository, PoseRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IDifficultyRepository, DifficultyRepository>();

// Register your use case
builder.Services.AddScoped<CreatePoseUseCase>();
builder.Services.AddScoped < GetAllPosesUseCase>();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();