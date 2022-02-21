using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PlanetariumModels;
using PlanetariumRepositories;
using PlanetariumService.Profiles;
using PlanetariumServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
/*var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new TicketProfile());
    mc.AddProfile(new HallProfile());
});

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);*/
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

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

builder.Host.UseDefaultServiceProvider(o =>
{
    o.ValidateOnBuild = true;
    o.ValidateScopes = true;
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
