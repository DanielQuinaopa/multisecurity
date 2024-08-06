using Cordillera.Distribuidas.Event;
using MediatR;
using Microsoft.EntityFrameworkCore;
using multitrabajo_deposito.Data;
using multitrabajo_deposito.Messages.CommandHandlers;
using multitrabajo_deposito.Messages.Commands;
using multitrabajo_deposito.Repositories;
using multitrabajo_deposito.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ContextDatabase>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
//Services

builder.Services.AddScoped<IServiceTransaction, ServiceTransaction>();
builder.Services.AddScoped<IServiceAccount, ServiceAccount>();
builder.Services.AddSingleton<IHttpClient, CustomHttpClient>();
//RabbitMQ

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
builder.Services.AddRabbitMQ();
builder.Services.AddTransient<IRequestHandler<TransactionCreateCommand, bool>, TransactionCommandHandler>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

//Create Database

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ContextDatabase>();
        //Funcion static crea usuarios
        DbInitializer.Initialize(context);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred creating the DB.");
    }
}

app.UseAuthorization();

app.MapControllers();

app.Run();
