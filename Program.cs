using System;
using BrainstormV2Backend.Services;
using BrainstormV2Backend.Services.Contracts;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<ITemplateService, TemplateService>();

var mongoConnectionString = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development"
  ? builder.Configuration["MONGODB_CONNECTION_STRING"]
  : Environment.GetEnvironmentVariable("MONGODB_CONNECTION_STRING");

builder.Services.AddSingleton<IMongoClient>(s =>
  new MongoClient(mongoConnectionString));

builder.Services.AddAuthentication(options =>
{
  options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
  options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
  options.Authority = $"https://{builder.Configuration["Auth0:Domain"]}/";
  options.Audience = builder.Configuration["Auth0:Audience"];
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
