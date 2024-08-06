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

  // userid is added prior to invoking this method
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

  // needs userid here
  public async Task<ClosedPnL> ClosedPnL(Guid userid) {
    return await closed_pnl.SingleOrDefaultAsync(u => u.userid == userid);
  }

  // needs userid here
  public async Task updateClosedPnL(decimal new_pnl, Guid user_id) {
    var closed = await closed_pnl.SingleOrDefaultAsync(p => p.userid == user_id);

    // create new closed pnl for user
    if (closed == null) {
      ClosedPnL new_closed = new ClosedPnL {
        userid = user_id,
        pnl = new_pnl
      };
      closed_pnl.Add(new_closed);
    } else {
      closed.pnl += new_pnl;
    }
    
    await SaveChangesAsync();
  }

  // need userid as each trade is unique to a user
  public async Task<List<Trade>> getTrades(Guid userid) {
    return await trades.Where(t => t.userid == userid).ToListAsync();
  }
}
