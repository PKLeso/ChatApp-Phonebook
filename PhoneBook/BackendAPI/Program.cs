using Microsoft.EntityFrameworkCore;
using PhoneBook.Data;
using PhoneBook.Extensions;
using PhoneBook.Extensions.HubConfig;
using Microsoft.Azure.SignalR;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

// Add Authentication
builder.Services.AddAuth(builder);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//SignalR
//builder.Services.AddSignalR(options =>
//{
//    options.EnableDetailedErrors = true;
//});
ConfigurationManager configuration = builder.Configuration;
builder.Services.AddSignalR(); //.AddAzureSignalR();

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

app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();

//SignalR
//app.UseAzureSignalR(app.Configuration.GetConnectionString("Azure:SignalRConnectionString"), 
//    route =>
//    {
//        route.UseHub<HubConfigExtension>("/chat");
//    });
//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapHub<HubConfigExtension>("/chat");
//});

app.MapHub<HubConfigExtension>("/chat");

app.MapControllers();

app.Run();
