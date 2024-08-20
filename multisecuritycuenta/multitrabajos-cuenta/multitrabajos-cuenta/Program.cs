using Consul;
using Cordillera.Distribuidas.Discovery.Consul;
using Cordillera.Distribuidas.Discovery.Fabio;
using Cordillera.Distribuidas.Discovery.Mvc;
using Microsoft.EntityFrameworkCore;
using multitrabajos_cuenta.Data;
using multitrabajos_cuenta.Repository;
using multitrabajos_cuenta.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//Service datacontext sql server

builder.Services.AddDbContext<ContextDatabase>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//Servicios
 
builder.Services.AddScoped<IServiceAccount, ServiceAccount>();

//Consul
builder.Services.AddSingleton<IServiceId, ServiceId>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddConsul();

//End Consul

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


app.UseAuthorization();

app.MapControllers();

app.Run();
