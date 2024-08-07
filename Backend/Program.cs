using Backend.Services.Trades;
using Backend.Services.Auth;
using Backend.Services.UserID;
using System.Text;
using Backend.Services.Positions;
using Backend.Data;
using Microsoft.EntityFrameworkCore;
using Backend.Services.ClosedPnLs;
using System.Net.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

builder.Services.AddAuthentication(cfg => {
  cfg.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
  cfg.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
  cfg.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x => {
  x.TokenValidationParameters = new TokenValidationParameters {
    ValidIssuer = config["JWT:Issuer"],
    ValidAudience = config["JWT:Audience"],
    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(config["JWT:Key"]!)),
    ValidateIssuer = true, 
    ValidateAudience = true,
    ValidateLifetime = true,
    ValidateIssuerSigningKey = true,
    ClockSkew = TimeSpan.Zero
  };
});

builder.Services.AddAuthorization();
builder.Services.AddHttpContextAccessor();

builder.Services.AddControllers();
builder.Services.AddScoped<ITradeService, TradeService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IClosedPnLService, ClosedPnLService>();
builder.Services.AddScoped<IPositionService, PositionService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddDbContext<InvestmentsDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDbContext<UserDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

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
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();