using Backend.Services.Trades;
using Backend.Services.Positions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddScoped<ITradeService, TradeService>();
builder.Services.AddScoped<IPositionService, PositionService>();
builder.Services.AddCors(options =>
{
  options.AddPolicy(name: "AllowReact",
    policy =>
      {
        policy.WithOrigins("http://localhost:5174/")
        .AllowAnyHeader()
        .AllowAnyMethod();
      });
});

var app = builder.Build();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseCors("AllowReact");
app.Run();
