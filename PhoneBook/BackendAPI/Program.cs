using Microsoft.EntityFrameworkCore;
using PhoneBook.Data;
using PhoneBook.Extensions;
using PhoneBook.Extensions.HubConfig;

var builder = WebApplication.CreateBuilder(args);

// Add Authentication
builder.Services.AddAuth(builder);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//SignalR
builder.Services.AddSignalR(options =>
{
    options.EnableDetailedErrors = true;
});

builder.Services.AddDbContext<PhonebookDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnectionString"));
});

// Enable CORS
builder.Services.AddCorsPolicy();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("ËnableCorsForAngularApp");


app.UseAuthentication();
app.UseAuthorization();

//SignalR
app.MapHub<HubConfigExtension>("/chat");

app.MapControllers();

app.Run();
