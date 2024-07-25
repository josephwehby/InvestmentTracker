using Microsoft.EntityFrameworkCore;
using Backend.Models;
using System.Drawing;
using System.Runtime.CompilerServices;
using Backend.Services.Trades;
namespace Backend.Data;

public class InvestmentsDbContext : DbContext {
  
  public DbSet<Trade> trades { get; set; }
  public DbSet<ClosedPnL> closed_pnl { get; set; }

  public InvestmentsDbContext(DbContextOptions<InvestmentsDbContext> options) : base(options) {}

  protected override void OnModelCreating(ModelBuilder modelBuilder) {
    base.OnModelCreating(modelBuilder);
  }

  public async Task<List<Trade>> getTradesByTicker(string ticker, Guid userid) {
    return await trades.Where(t => t.ticker == ticker && t.userid == userid).OrderBy(t => t.purchase_day).ToListAsync(); 
  }

  public async Task addTrade(Trade trade) {
    trades.Add(trade);
    await SaveChangesAsync();
  }

  // this is getting the trade object so need to for userid here as well
  public async Task<bool> deleteTrade(Trade trade) {
    if (trade == null) return false;

    trades.Remove(trade);
    await SaveChangesAsync();
    return true;
  }


  // since it each trade has unique id userid will not be needed most likely
  public async Task<bool> updateTradeShares(int id, decimal new_share_count) {
    var trade = await trades.FindAsync(id);
    if (trade == null) return false;

    trade.shares = new_share_count;
    await SaveChangesAsync();
    return true;
  }

  public async Task<ClosedPnL> ClosedPnL(Guid userid) {
    return await closed_pnl.SingleOrDefaultAsync(u => u.userid == userid);
  }

  public async Task<bool> updateClosedPnL(decimal new_pnl, Guid userid) {
    var closed = await closed_pnl.SingleOrDefaultAsync(p => p.userid == userid);
    
    if (closed == null) return false;
    
    closed.pnl += new_pnl;
    await SaveChangesAsync();
    return true;
  }

  public async Task<List<Trade>> getTrades(Guid userid) {
    return await trades.Where(t => t.userid == userid).ToListAsync();
  }
}