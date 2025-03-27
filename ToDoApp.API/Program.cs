using Microsoft.EntityFrameworkCore;
using FluentValidation;
using FluentValidation.AspNetCore;
using ToDoApp.Application.Validations;
using ToDoApp.Application.Interfaces.Repositories;
using ToDoApp.Infrastructure.Persistence;
using ToDoApp.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAnyOrigin", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddValidatorsFromAssemblyContaining<TaskValidator>();
builder.Services.AddFluentValidationAutoValidation();


// Configurar SQL Server
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Inyectar repositorio
builder.Services.AddScoped<ITaskRepository, TaskRepository>();

var app = builder.Build();

app.UseCors("AllowAnyOrigin");
app.UseRouting();


app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();
app.MapControllers();

app.Run();