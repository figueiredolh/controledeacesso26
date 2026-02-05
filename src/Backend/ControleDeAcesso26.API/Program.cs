using ControleDeAcesso26.API.Filters;
using ControleDeAcesso26.Application.DependencyInjection;
using ControleDeAcesso26.Infrastructure.DependencyInjection;
using ControleDeAcesso26.Infrastructure.Migrations;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ExceptionsFilter>();
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfrastructureLayer(builder.Configuration);
builder.Services.AddApplicationLayer();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapSwagger("/openapi/{documentName}.json");
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
DatabaseMigration.MigrateDatabase(builder.Configuration, scope.ServiceProvider);

app.Run();
