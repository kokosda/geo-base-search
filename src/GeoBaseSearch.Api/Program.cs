using GeoBaseSearch.Api.Extensions;
using GeoBaseSearch.Application.DependencyInjection;
using GeoBaseSearch.Infrastructure.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddInfrastructureLevelServices();
builder.Services.AddApplicationLevelServices();

builder.Services.AddResponseCaching(configureOptions => configureOptions.UseCaseSensitivePaths = true);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseInitialization();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors(policyBuilder => policyBuilder.AllowAnyOrigin());
}

app.UseHttpsRedirection();
app.UseResponseCaching();
app.UseResponseCachingSettings();
app.UseAuthorization();

app.MapControllers();

app.Run();