using System.Data;
using MySql.Data.MySqlClient;
using YogaApp.Application.RespositoryInterfaces;
using YogaApp.Application.Services;
using YogaApp.Application.UseCaseInterfaces;
using YogaApp.Application.UseCases;
using YogaApp.Application.UseCases.GetDifficultyByDiffId;
using YogaApp.Application.UseCases.UpdatePose;
using YogaApp.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IDbConnection>(s => 
{
    var connectionString = builder.Configuration.GetConnectionString("yogaApi");
    return new MySqlConnection(connectionString);
});
// Register repositories
builder.Services.AddScoped<IPoseRepository, PoseRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IDifficultyRepository, DifficultyRepository>();

// Register use cases
builder.Services.AddScoped<ICreatePoseInDbUseCase, CreatePoseInDbUseCase>();
builder.Services.AddScoped<IGetAllCategoriesUseCase, GetAllCategoriesUseCase>();
builder.Services.AddScoped<IGetAllDifficultiesUseCase, GetAllDifficultiesUseCase>();
builder.Services.AddScoped<IGetAllPosesUseCase, GetAllPosesUseCase>();
builder.Services.AddScoped<IGetPoseByIdUseCase, GetPoseByIdUseCase>();
builder.Services.AddScoped<IGetCatByCatIdUseCase, GetCatByCatIdUseCase>();
builder.Services.AddScoped<IGetDifficultyByIdUseCase, GetDifficultyByIdUseCase>();
builder.Services.AddScoped<IUpdatePoseUseCase, UpdatePoseUseCase>();

//Register pose use case services facade
builder.Services.AddScoped<IApplicationServices, ApplicationService>();

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