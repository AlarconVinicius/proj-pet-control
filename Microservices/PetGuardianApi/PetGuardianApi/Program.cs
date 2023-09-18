using Microsoft.EntityFrameworkCore;
using PetGuardianApi.Data.Configuration;
using PetGuardianApi.Database.Repositories;
using PetGuardianApi.Domain.Interfaces.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
var dbName = Environment.GetEnvironmentVariable("DB_NAME");
var dbPassword = Environment.GetEnvironmentVariable("DB_SA_PASSWORD");
var connectionString = $"Data Source={dbHost};Initial Catalog={dbName};User ID=sa;Password={dbPassword};Encrypt=False;TrustServerCertificate=False";
builder.Services.AddDbContext<BaseDbContext>(
opt =>
		opt.UseSqlServer(connectionString)
);

builder.Services.AddScoped<IPetGuardianRepository, PetGuardianRepository>();

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
