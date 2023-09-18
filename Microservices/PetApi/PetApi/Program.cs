using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PetApi.Data.Configuration;
using PetApi.Database.Repositories;
using PetApi.Domain.Interfaces.Repositories;
using PetApi.Domain.Interfaces.Services;
using PetApi.Domain.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
	var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
	var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
	options.IncludeXmlComments(xmlPath);
	options.SwaggerDoc("v1", new OpenApiInfo { Title = "Pet.Api", Version = "v1" });
});

var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
var dbName = Environment.GetEnvironmentVariable("DB_NAME");
var dbPassword = Environment.GetEnvironmentVariable("DB_SA_PASSWORD");
var connectionString = $"Data Source={dbHost};Initial Catalog={dbName};User ID=sa;Password={dbPassword};Encrypt=False;TrustServerCertificate=False";
builder.Services.AddDbContext<BaseDbContext>(
opt =>
		opt.UseSqlServer(connectionString)
);

builder.Services.AddScoped<IPetGuardianRepository, PetGuardianRepository>();

builder.Services.AddScoped<IPetGuardianService, PetGuardianService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using var scope = app.Services.CreateScope();
var baseDbContext = scope.ServiceProvider.GetRequiredService<BaseDbContext>();
if (baseDbContext.Database.GetPendingMigrations().Any())
{
	baseDbContext.Database.Migrate();
}

app.Run();
