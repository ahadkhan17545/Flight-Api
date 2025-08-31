using FlightApi.Data;
using FlightApi.Repositories;
using FlightApi.Services;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

// Entry point for the Flight API application.
// Configures services, middleware, and the HTTP request pipeline.

var builder = WebApplication.CreateBuilder(args);

// -------------------------
// Configure Services
// -------------------------
builder.Services.AddDbContext<FlightContext>(options =>
    options.UseInMemoryDatabase("FlightsDb")); // Use in-memory database for testing

builder.Services.AddScoped<IFlightRepository, FlightRepository>();
builder.Services.AddScoped<IFlightService, FlightService>();

builder.Services.AddControllers();

// Enable Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    // Configure Swagger doc
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Flight API",
        Version = "v1",
        Description = "REST API for managing flight information"
    });

    // Include XML comments
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

// -------------------------
// Configure Middleware
// -------------------------
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    // Centralized exception handling for production
    app.UseExceptionHandler("/error");
}

app.UseHttpsRedirection();

app.MapControllers(); // Map API controllers

app.Run(); // Run the application
