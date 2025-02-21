using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Web.App;
using Web.App.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.Configure<RouteOptions>(options => { options.LowercaseUrls = true; });

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Wafi Sample Test API",
        Version = "v1",
        Description = "API for CarBooking",
        Contact = new OpenApiContact
        {
            Name = "IAFahim",
            Email = "iafahim.dev@gmail.com",
            Url = new Uri("https://github.com/IAFahim")
        },
        License = new OpenApiLicense
        {
            Name = "MIT License",
            Url = new Uri("https://opensource.org/licenses/MIT")
        }
    });

    // options.MapType<DateTime>(() => new OpenApiSchema
    // {
    //     Type = "string",
    //     Format = "DateTime",
    //     Example = new OpenApiString(DateTime.UtcNow.ToString("yyyy-MM-dd-HH-mm-ss")),
    //     Description = $"DateTime in yyyy-MM-dd-HH-mm-ss format (e.g., {DateTime.UtcNow:yyyy-MM-dd-HH-mm-ss})"
    // });
});


builder.Services.AddResponseCompression(options => { options.EnableForHttps = true; });

builder.Services.AddScoped<IBookingService, BookingService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();