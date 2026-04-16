using DebitosTributarios.Application.Services;
using DebitosTributarios.Domain.Interfaces;
using DebitosTributarios.Infrastructure.Contexts;
using DebitosTributarios.Infrastructure.Repositories;
using DebitosTributarios.Infrastructure.Settings;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//Logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddControllers();

builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen(opts =>
{
    opts.SwaggerDoc("v1", new() { Title = "Débitos Tributários API", Version = "v1" });
});

builder.Services.AddDbContext<DebitosTributariosDbContext>(options =>
{
    var rawConnectionString = builder.Configuration.GetConnectionString("Sqlite")
        ?? "Data Source=App_Data/debitos-tributarios.db";

    var settings = SqliteDatabaseSettings.Create(rawConnectionString, builder.Environment.ContentRootPath);

    Directory.CreateDirectory(Path.GetDirectoryName(settings.DatabasePath)!);

    options.UseSqlite(settings.ConnectionString);
});

builder.Services.AddScoped<IContribuinteRepository, ContribuinteRepository>();
builder.Services.AddScoped<IDebitoRepository, DebitoRepository>();

builder.Services.AddScoped<IContribuinteApplicationService, ContribuinteApplicationService>();
builder.Services.AddScoped<IDebitoApplicationService, DebitoApplicationService>();

var app = builder.Build();

// EnsureCreated cria as tabelas se não existirem
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<DebitosTributariosDbContext>();
    db.Database.EnsureCreated();
}

// swagger
app.MapOpenApi();
app.UseSwaggerUI(options =>
{
    options.RoutePrefix = "swagger";
    options.SwaggerEndpoint("/openapi/v1.json", "Débitos Tributários API v1");
    options.DocumentTitle = "Débitos Tributários API";
});

app.MapControllers();

app.Run();

public partial class Program { }
