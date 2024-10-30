using Microsoft.EntityFrameworkCore;
using TaskManager.Api.Data;
using TaskManager.Api.Mappings;
using TaskManager.Api.Repositories.Interfaces;
using TaskManager.Api.Repositories.Implementations;
using TaskManager.Api.Services.Interfaces;
using TaskManager.Api.Services.Implementations;

var builder = WebApplication.CreateBuilder(args);


// Configura o contexto do banco de dados.
builder.Services.AddDbContext<TaskManagerDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configuração do AutoMapper
// builder.Services.AddAutoMapper(typeof(Program));

// Injeção de dependências para os repositórios
builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<ITaskRepository, TaskRepository>();

// Injeção de dependência para os serviços
builder.Services.AddScoped<IPersonService, PersonService>();
builder.Services.AddScoped<ITaskService, TaskService>();

// Configuração de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAnyOrigin",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

// Configuração do AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile)); // Aquí registra el perfil de mapeo

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// No método Configure (se você usar .NET Core 3.1 ou 5)
app.UseCors("AllowAnyOrigin");


// Configuração do Swagger em ambientes de desenvolvimento e produção
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
        c.RoutePrefix = "swagger"; // Estabelecer a rota base para a UI do Swagger.
    });
}


// Configure o pipeline de solicitações HTTP
if (app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseHttpsRedirection();


app.MapControllers();
app.Run();