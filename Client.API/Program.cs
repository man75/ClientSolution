using ClientSi.API.Mapping;
using ClientSi.APPLICATION.Services;
using ClientSi.APPLICATION.UseCases.AddClient;
using ClientSi.Domain.Abstractions;

using ClientSi.INFRASTRUCTURE.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// DbContext
builder.Services.AddDbContext<ClientContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ClientDb")));

// Notification
builder.Services.AddScoped<Notification>();

// Repository
builder.Services.AddScoped<IClientRepository, ClientRepository>();

// Use case + décorateur
builder.Services.AddScoped<AddClientUseCase>();
builder.Services.AddScoped<IAddClientUseCase>(sp =>
{
    var notification = sp.GetRequiredService<Notification>();
    var inner = sp.GetRequiredService<AddClientUseCase>();
    return new AddClientUseCaseValidation(notification, inner);
});

// AutoMapper
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AllowNullCollections = true;
}, typeof(Program)); // Le type vient APRES l'action


builder.Services.AddDbContext<ClientContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("ClientDb")
    ));

// Controllers
builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();

app.Run();
