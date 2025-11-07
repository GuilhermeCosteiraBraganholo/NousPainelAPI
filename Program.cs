using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using NousPainelAPI.Data;
using NousPainelAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Banco em memória para facilitar correção (não persiste em disco)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("NousDB"));

// Serviços
builder.Services.AddScoped<IAlunoService, AlunoService>();

// Controllers + Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "NOUS Painel API",
        Version = "v1",
        Description = "Sprint 2 (.NET): rota de search + HATEOAS",
        Contact = new OpenApiContact { Name = "Equipe NOUS" }
    });
});

var app = builder.Build();

// Swagger sempre habilitado para facilitar a correção
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "NOUS Painel API v1");
    c.RoutePrefix = "swagger";
});

// Redireciona / para o Swagger
app.MapGet("/", context =>
{
    context.Response.Redirect("/swagger/index.html");
    return Task.CompletedTask;
});

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
