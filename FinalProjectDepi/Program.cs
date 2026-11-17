using FinalProjectDepi.DAL.Repositories.Classes;
using FinalProjectDepi.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddScoped<IPhoneRepo, PhoneRepo>();
builder.Services.AddScoped<IClientRepo, ClientRepo>();

builder.Services.AddDbContext<FinalProjectDepi.DAL.Contexts.AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}
app.MapGet("/", () => Results.Redirect("/scalar")).ExcludeFromDescription();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();