using Microsoft.AspNetCore.Hosting;
using Taller.API.Mapper;
using Taller.Contracts.Managers;
using Taller.Contracts.Services;
using Taller.Domain;
using Taller.Managers;
using Taller.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(CarMapperProfile));

builder.Services.AddTransient<ICarManager, CarManager>();
builder.Services.AddTransient<ICarService, CarService>();

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
