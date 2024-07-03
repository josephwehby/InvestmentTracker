using Microsoft.EntityFrameworkCore;
using Backend.Models;
using System.Drawing;
using System.Runtime.CompilerServices;
namespace Backend.Data;

public class InvestmentsDbContext : DbContext {
  
  public DbSet<Trade> trades { get; set; }
  public DbSet<ClosedPnL> closed_pnl { get; set; }

  public InvestmentsDbContext(DbContextOptions<InvestmentsDbContext> options) : base(options) {}

  protected override void OnModelCreating(ModelBuilder modelBuilder) {
    base.OnModelCreating(modelBuilder);
  }

  public async Task<List<Trade>> getTradesByTicker(string ticker) {
    return await trades.Where(t => t.ticker == ticker).OrderBy(t => t.purchase_day).ToListAsync(); 
  }

  public async Task<bool> deleteTrade(Trade trade) {
    if (trade == null) return false;

    trades.Remove(trade);
    await SaveChangesAsync();
    return true;
  }

  public async Task<bool> updateTradeShares(int id, decimal new_share_count) {
    var trade = await trades.FindAsync(id);
    if (trade == null) return false;

    trade.shares = new_share_count;
    await SaveChangesAsync();
    return true;
  }

  public async Task<ClosedPnL> ClosedPnL() {
    return await closed_pnl.SingleOrDefaultAsync();
  }

  public async Task<bool> updateClosedPnL(decimal new_pnl) {
    var closed = await closed_pnl.SingleOrDefaultAsync();
    
    if (closed == null) return false;
    
    closed.pnl += new_pnl;
    await SaveChangesAsync();
    return true;
  }
}