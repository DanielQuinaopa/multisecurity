using Cordillera.Distribuidas.Event.Bus;
using Cordillera.Distribuidas.Event;
using multitrabajos_history;
using multitrabajos_history.Messages.Events;
using multitrabajos_history.Messages.EventsHandlers;
using multitrabajos_history.Repositories;
using System.Reflection;
using multitrabajos_history.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.Configure<Mongosettings>(opt =>
{
    opt.Connection = builder.Configuration.GetSection("mongo:cn").Value;
    opt.DatabaseName = builder.Configuration.GetSection("mongo:database").Value;
});

//Services
builder.Services.AddScoped<IServiceHistory, ServiceHistory>();
builder.Services.AddScoped<IMongoBookDBContext, MongoBookDBContext>();

//RabbitMQ

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
//builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddRabbitMQ();
builder.Services.AddTransient<TransactionEventHandler>();
builder.Services.AddTransient<IEventHandler<TransactionCreatedEvent>, TransactionEventHandler>();

//EndRabbitMQ

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


app.UseAuthorization();

app.MapControllers();

var eventBus = app.Services.GetRequiredService<IEventBus>();
eventBus.Subscribe<TransactionCreatedEvent, TransactionEventHandler>();

app.Run();
