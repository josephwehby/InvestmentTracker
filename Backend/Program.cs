using Backend.Services.Trades;
using Backend.Services.Positions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddScoped<ITradeService, TradeService>();
builder.Services.AddScoped<IPositionService, PositionService>();

var app = builder.Build();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
