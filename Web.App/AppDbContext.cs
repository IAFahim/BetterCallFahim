using Microsoft.EntityFrameworkCore;
using Web.App.Entities;

namespace Web.App;

public class AppDbContext(DbContextOptions<AppDbContext> contextOptions) : DbContext(contextOptions)
{
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<Car> Cars { get; set; }
}