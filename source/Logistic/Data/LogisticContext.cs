using Logistic.DbModels;
using Microsoft.EntityFrameworkCore;

namespace Logistic.Data
{
  public class LogisticContext : DbContext
  {
    public LogisticContext(DbContextOptions<LogisticContext> options) : base(options)
    {
    }

    public DbSet<Truck> Trucks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Truck>().ToTable("Truck");
    }
  }
}
