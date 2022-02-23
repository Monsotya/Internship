using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PlanetariumModels;
using PlanetariumRepositories;
using PlanetariumService.Profiles;
using PlanetariumServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Planetarium service",
        Description = "A simple example ASP.NET Core Web API",        
    });
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});
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

builder.Host.UseDefaultServiceProvider(o =>
{
    o.ValidateOnBuild = true;
    o.ValidateScopes = true;
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(c =>
    {
        c.SerializeAsV2 = true;
    });
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
