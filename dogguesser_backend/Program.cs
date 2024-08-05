using dogguesser_backend.Data;
using dogguesser_backend.Models.DTO;
using dogguesser_backend.Models;
using Microsoft.EntityFrameworkCore;
using dogguesser_backend.Service;


var builder = WebApplication.CreateBuilder(args);
// Configure Entity Framework Core with SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


//CORS CORS CORS CORS CORS CORS CORS CORS CORS CORS CORS CORS CORS CORS CORS 
builder.Services.AddCors(co => {
    co.AddPolicy("CORS" , pb =>{
        pb.WithOrigins("http://127.0.0.1:5501")
        .AllowAnyHeader();
    });
});

// Add services to the container.
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IScoreService, ScoreService>();


builder.Services.AddControllers();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();




//Adding DbContext

     

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//CORS CORS CORS CORS CORS CORS CORS CORS CORS CORS CORS CORS CORS CORS CORS
app.UseCors("CORS"); //<-USE CORS with your policy name
//CORS CORS CORS CORS CORS CORS CORS CORS CORS CORS CORS CORS CORS CORS CORS

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
