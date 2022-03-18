using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PlanetariumModels;
using PlanetariumRepositories;
using PlanetariumService.Hubs;
using PlanetariumService.Profiles;
using PlanetariumServices;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new OneProfile());
});

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddDbContext<PlanetariumServiceContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("PlanetariumServiceContext"), builder => builder.EnableRetryOnFailure()));
builder.Services.AddTransient<PlanetariumServiceContext>();

builder.Services.AddTransient(typeof(IRepository<>), typeof(Repository<>));

builder.Services.AddTransient<ITicketRepository, TicketRepository>();
builder.Services.AddTransient<IPosterRepository, PosterRepository>();
builder.Services.AddTransient<IHallRepository, HallRepository>();
builder.Services.AddTransient<ITicketRepository, TicketRepository>();

builder.Services.AddTransient<ITicketService, TicketService>();
builder.Services.AddTransient<IPosterService, PosterService>();
builder.Services.AddTransient<IHallService, HallService>();
builder.Services.AddTransient<IPerformanceService, PerformanceService>();
builder.Services.AddTransient<IOrderService, OrderService>();

builder.Services.AddSignalR();

builder.Host.UseDefaultServiceProvider(o =>
{
    o.ValidateOnBuild = true;
    o.ValidateScopes = true;
});


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");

    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapHub<BuyingTicketsHub>("/buyingTickets");

app.Run();
