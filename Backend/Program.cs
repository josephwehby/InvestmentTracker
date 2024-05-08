using Backend.Services.Investments;

var builder = WebApplication.CreateBuilder(args);
{
  builder.Services.AddControllers();
  builder.Services.AddScoped<IInvestmentService, InvestmentService>();
}

var app = builder.Build();
{
  app.UseHttpsRedirection();
  app.UseAuthorization();
  app.MapControllers();
  app.Run();
}
