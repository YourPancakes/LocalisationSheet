using FluentValidation;
using LocalisationSheet.Server.Api.Middleware;
using LocalisationSheet.Server.Application.Services.Interfaces;
using LocalisationSheet.Server.Domain.Interfaces;
using LocalisationSheet.Server.Infrastructure.Data;
using LocalisationSheet.Server.Infrastructure.Repository;
using LocalisationSheet.Server.Mappings;
using LocalisationSheet.Server.Services.Implementations;
using LocalisationSheet.Server.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            policy.WithOrigins("http://localhost:5173", "http://localhost:5174", "http://localhost:3000", "http://localhost", "http://frontend")
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials();
        });
});

builder.Services.AddValidatorsFromAssemblyContaining<Program>();

builder.Services.AddDbContext<LocalizationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddScoped<IKeyRepository, KeyRepository>();
builder.Services.AddScoped<ILanguageRepository, LanguageRepository>();
builder.Services.AddScoped<ITranslationRepository, TranslationRepository>();

builder.Services.AddScoped<IKeyService, KeyService>();
builder.Services.AddScoped<ILanguageService, LanguageService>();
builder.Services.AddScoped<ITranslationService, TranslationService>();
builder.Services.AddScoped<ILocalizationTableService, LocalizationTableService>();

builder.Services.AddControllers();

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo 
    { 
        Title = "LocalisationSheet API", 
        Version = "v1",
        Description = "API for managing localization keys and translations"
    });
    
    c.AddServer(new OpenApiServer
    {
        Url = "http://localhost:5000",
        Description = "Development server"
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "LocalisationSheet API V1");
        c.RoutePrefix = "swagger";
    });
}

app.UseCors("AllowFrontend");

app.UseRouting();
app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseDefaultFiles();
app.UseStaticFiles();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var logger = services.GetRequiredService<ILogger<Program>>();
    try
    {
        var context = services.GetRequiredService<LocalizationDbContext>();
        context.Database.Migrate(); 

        await DbSeeder.SeedAsync(context);  
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "Error initializing the database");
        throw;
    }
}


app.MapGet("/ping", () => Results.Ok("pong"));

app.Run();