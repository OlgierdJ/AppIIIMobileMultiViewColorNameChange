using Microsoft.EntityFrameworkCore;
using CoreLib.Interfaces.Services;
using TodoWebApi.EFCore;
using TodoWebApi.Repositories;
using TodoWebApi.Services;
using System.Text.Json.Serialization;
using TodoWebApi.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient(typeof(IIndividualRepository), typeof(IndividualRepository));

builder.Services.AddTransient(typeof(IIndividualService), typeof(IndividualService));
builder.Services.AddTransient(typeof(IPushNotificationService), typeof(PushNotificationService));
var ConnectionString = builder.Configuration.GetConnectionString("DbConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<IndividualDbContext>(options =>
    options.UseSqlServer(ConnectionString));
builder.Services.AddDataProtection();
builder.Services.AddAuthorization();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers().AddJsonOptions(opt =>
{
    opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
});
builder.Services.AddSignalR(options =>
{
    options.KeepAliveInterval = TimeSpan.FromMinutes(0);
}).AddJsonProtocol(protocol =>
{
    protocol.PayloadSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.MapHub<ClientHub>("/ClientChat");
app.MapControllers();

app.Run();
