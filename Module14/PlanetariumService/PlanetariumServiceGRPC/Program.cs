using Microsoft.EntityFrameworkCore;
using PlanetariumModels;
using PlanetariumRepositories;
using PlanetariumServiceGRPC.Services;
using PlanetariumServices;

var builder = WebApplication.CreateBuilder(args);

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
builder.Services.AddGrpc();

builder.Services.AddDbContext<PlanetariumServiceContext>(options => options.UseSqlServer("data source=.; database=Planetarium; integrated security=SSPI", builder => builder.EnableRetryOnFailure()));
builder.Services.AddTransient<PlanetariumServiceContext>();

builder.Services.AddTransient(typeof(IRepository<>), typeof(Repository<>));

builder.Services.AddTransient<ITicketRepository, TicketRepository>();

builder.Services.AddTransient<ITicketService, TicketService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<PlanetariumService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
app.UseHttpsRedirection();
app.Run();
