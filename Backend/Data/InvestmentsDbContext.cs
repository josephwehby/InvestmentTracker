using Microsoft.EntityFrameworkCore;
using Backend.Models;
namespace Backend.Data;

public class InvestmentsDbContext : DbContext {
  public DbSet<Trade> trades { get; set; }
  public InvestmentsDbContext(DbContextOptions<InvestmentsDbContext> options) : base(options) {}

  protected override void OnModelCreating(ModelBuilder modelBuilder) {
    base.OnModelCreating(modelBuilder);
  }
}