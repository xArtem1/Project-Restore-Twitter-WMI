using BikolTwitter.Database;
using BikolTwitter.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var connString = builder.Configuration.GetConnectionString("DBConnection");
builder.Services.AddDbContext<BikolTwitterDbContext>(o => o.UseSqlite(connString));
builder
.Services
.AddScoped<IBikolSubService, BikolSubService>()
.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly())
.AddFluentValidationAutoValidation()
.AddAutoMapper(Assembly.GetExecutingAssembly());
               

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var servicePropvider = app.Services.CreateScope().ServiceProvider;
var dbContext = servicePropvider.GetService<BikolTwitterDbContext>();

if (dbContext!.Database.IsRelational() && dbContext.Database.GetPendingMigrations().Any())
{
    dbContext.Database.Migrate();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }