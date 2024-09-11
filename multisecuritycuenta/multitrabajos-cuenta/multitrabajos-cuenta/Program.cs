using System.Reflection;
using Consul;
using Cordillera.Distribuidas.Discovery.Consul;
using Cordillera.Distribuidas.Discovery.Fabio;
using Cordillera.Distribuidas.Discovery.Mvc;
using Cordillera.Distribuidas.Event;
using Cordillera.Distribuidas.Event.Bus;
using Microsoft.EntityFrameworkCore;
using multitrabajos_cuenta.Data;
using multitrabajos_cuenta.Messages.Events;
using multitrabajos_cuenta.Repository;
using multitrabajos_cuenta.Services;
using multitrabajos_history.Messages.EventsHandlers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});
//Service datacontext sql server

builder.Services.AddDbContext<ContextDatabase>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
Console.Write(builder.Configuration.GetConnectionString("DefaultConnection"));
//Servicios

builder.Services.AddScoped<IServiceAccount, ServiceAccount>();

//Consul
builder.Services.AddSingleton<IServiceId, ServiceId>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddConsul();

//End Consul
//RabbitMQ

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
//builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddRabbitMQ();
builder.Services.AddTransient<UsuerEventHandler>();
builder.Services.AddTransient<IEventHandler<UsuerCreatedEvent>, UsuerEventHandler>();
builder.Services.AddFabio();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ContextDatabase>();
        //Funcion static crea usuarios
        DBInitializer.Initialize(context);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred creating the DB.");
    }
}

app.UseCors();
app.UseAuthorization();

app.MapControllers();


var eventBus = app.Services.GetRequiredService<IEventBus>();
eventBus.Subscribe<UsuerCreatedEvent, UsuerEventHandler>();
app.Run();
