using BankingAPI.DataAcess.DataContext;
using BankingAPI.DataAcess.Interface;
using BankingAPI.DataAcess.Repository;
using BankingAPI.DataAcess.Utilities;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<SimpleBankingContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SimpleBanking"));
});
builder.Services.AddScoped<ISimpleBankingRepository, SimpleBankingRepository>();
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
