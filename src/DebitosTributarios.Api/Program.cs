using DebitosTributarios.Api.Endpoints;
using DebitosTributarios.Domain.Interfaces;
using DebitosTributarios.Infrastructure.Contexts;
using DebitosTributarios.Infrastructure.Repositories;
using DebitosTributarios.Infrastructure.Settings;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DebitosTributariosDbContext>(options =>
{
    var rawConnectionString = "Data Source=App_Data/debitos-tributarios.db"!;
    var settings = SqliteDatabaseSettings.Create(rawConnectionString, builder.Environment.ContentRootPath);

    Directory.CreateDirectory(Path.GetDirectoryName(settings.DatabasePath)!);

    options.UseSqlite(settings.ConnectionString);
});

//builder.Services.AddScoped<IUnitOfWork, SqliteUnitOfWork>();
builder.Services.AddScoped<IContribuinteRepository, ContribuinteRepository>();
//builder.Services.AddScoped<IDebitoRepository, DebitoRepository>();

var app = builder.Build();

app.MapOpenApi();
app.UseSwaggerUI(options =>
{
    options.RoutePrefix = "swagger";
    options.SwaggerEndpoint("/openapi/v1.json", "Debitos Tributarios API v1");
    options.DocumentTitle = "Debitos Tributarios API";
});

app.MapControllers();

app.Run();
