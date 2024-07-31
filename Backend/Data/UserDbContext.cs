using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data;

public class UserDbContext : DbContext {
  public DbSet<User> users { get; set; }

  public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }
  
  protected override void OnModelCreating(ModelBuilder modelBuilder) {
    base.OnModelCreating(modelBuilder);
  }

  public async Task<User> getUserFromRefreshToken(string token) {
    return await users.FirstOrDefaultAsync(u => u.refresh_token == token);
  }

  public async Task<User> getUser(string username) {
    return await users.FirstOrDefaultAsync(u => u.username == username);
  }

  public async Task addUser(User user) {
    users.Add(user);
    await SaveChangesAsync();
  } 

  public async Task<User> getUserFromID(Guid userid) {
    var user = await users.FindAsync(userid);
    return user;
  } 

  public async Task setRefreshToken(Guid userid, string refresh, DateTime created, DateTime expires) {
    var user = await users.FindAsync(userid);
    if (user == null) {
      return;
    }

    user.refresh_token = refresh;
    user.token_created = created;
    user.token_expires = expires;

    await SaveChangesAsync();
  }
}