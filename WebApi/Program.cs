using Microsoft.EntityFrameworkCore;
using CoreLib.Interfaces.Services;
using TodoWebApi.EFCore;
using TodoWebApi.Repositories;
using TodoWebApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient(typeof(IIndividualRepository), typeof(IndividualRepository));

builder.Services.AddTransient(typeof(IIndividualService), typeof(IndividualService));
//builder.Services.AddTransient(typeof(IPushNotificationService), typeof(PushNotificationService));
var ConnectionString = builder.Configuration.GetConnectionString("DbConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<IndividualDbContext>(options =>
    options.UseSqlServer(ConnectionString));
builder.Services.AddDataProtection();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
