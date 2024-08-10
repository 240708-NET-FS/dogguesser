using dogguesser_backend.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using dogguesser_backend.Data;
using dogguesser_backend.Auth;

var builder = WebApplication.CreateBuilder(args);

// Configure Entity Framework Core with SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// CORS configuration
builder.Services.AddCors(co => {
    co.AddPolicy("CORS", pb => {
        pb.WithOrigins("http://127.0.0.1:5501")
          .AllowAnyHeader();
    });
});

// Register the private key as a configuration value
builder.Services.AddSingleton("YourPrivateKeyHere");
// Add services to the container
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IScoreService, ScoreService>();
builder.Services.AddScoped<IAuthService, AuthService>(); // Replace AuthService with the actual implementation


builder.Services.AddControllers();
builder.Services.AddAuthorization();

// Learn more about configuring Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// CORS configuration
app.UseCors("CORS");

// Middleware
app.UseHttpsRedirection();
app.UseAuthentication(); // Ensure this is placed before UseAuthorization
app.UseAuthorization();

app.MapControllers();

app.Run();
