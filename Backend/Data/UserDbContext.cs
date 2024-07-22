using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data;

public class UserDbContext : DbContext {
  public DbSet<User> users { get; set; }

  public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }
  
  protected override void OnModelCreating(ModelBuilder modelBuilder) {
    base.OnModelCreating(modelBuilder);
  }

  public async Task<User> getUser(string username) {
    return await users.FirstOrDefaultAsync(u => u.username == username);
  }

  public async Task addUser(User user) {
    users.Add(user);
    await SaveChangesAsync();
  }  

}