using FluentValidation;
using FluentValidation.AspNetCore;
using HomeApi.Contracts.Validators;
using HomeApi.Data;
using HomeApi.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using WebAPISecondASP.Configurations;
using WebAPISecondASP.Maps;

var builder = WebApplication.CreateBuilder(args);
var assembly = Assembly.GetAssembly(typeof(MappingProfile));
builder.Configuration
    .AddJsonFile("appsettings.json")
    .AddJsonFile("HomeOptions.json");

builder.Services.AddScoped<IDeviceRepository, DeviceRepository>();
builder.Services.AddScoped<IRoomRepository, RoomRepository>();

var stringConnection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services
    .AddDbContext<HomeApiContext>( options => options.UseSqlServer(stringConnection));

builder.Services.AddAutoMapper(assembly);

builder.Services.AddValidatorsFromAssemblyContaining<AddDeviceRequestValidator>();
builder.Services.AddFluentValidationAutoValidation();

// Add services to the container.
builder.Services.Configure<HomeOptions>(builder.Configuration);
builder.Services.Configure<Address>(builder.Configuration.GetSection("Address"));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddControllers();

builder.Services.AddSwaggerGen(c =>
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "HomeApi",
        Version = "v1"
    }));

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
