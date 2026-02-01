using HelloKenzo.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace HelloKenzo.Web.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Registration> Registrations { get; set; }
}
