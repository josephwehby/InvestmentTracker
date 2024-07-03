using Backend.Services.Trades;
using Backend.Services.Positions;
using Backend.Data;
using Microsoft.EntityFrameworkCore;
using Backend.Services.ClosedPnLs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddScoped<ITradeService, TradeService>();
builder.Services.AddScoped<IClosedPnLService, ClosedPnLService>();
builder.Services.AddScoped<IPositionService, PositionService>();
builder.Services.AddDbContext<InvestmentsDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddCors(options =>
{
  options.AddPolicy(name: "AllowReact",
    policy =>
      {
        //policy.WithOrigins("http://localhost:5173").AllowAnyHeader().AllowAnyMethod();
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
      });
});

var app = builder.Build();
app.UseHttpsRedirection();
app.UseCors("AllowReact");
app.UseAuthorization();
app.MapControllers();
app.Run();
