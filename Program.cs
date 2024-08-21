using System.Reflection;
using AgriWeatherTracker.Data;
using AgriWeatherTracker.Repository;
using AgriWeatherTracker.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

try
{
    var builder = WebApplication.CreateBuilder(args);
    builder.Logging.AddConsole();

    // Add services to the container.
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    // Add AutoMapper
    builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

    // Add scoped services
    builder.Services.AddScoped<ICropRepository, CropRepository>();
    builder.Services.AddScoped<IWeatherRepository, WeatherRepository>();
    builder.Services.AddScoped<ILocationRepository, LocationRepository>();
    builder.Services.AddScoped<IConditionThresholdRepository, ConditionThresholdRepository>();
    builder.Services.AddScoped<IGrowthCycleRepository, GrowthCycleRepository>();
    builder.Services.AddScoped<IGrowthStageRepository, GrowthStageRepository>();
    builder.Services.AddScoped<IHealthScoreRepository, HealthScoreRepository>();
    builder.Services.AddScoped<ISignalGeneratedRepository, SignalGeneratedRepository>();
    builder.Services.AddScoped<WeatherHealthService>();
    builder.Services.AddScoped<HealthEvaluatorService>();
    builder.Services.AddTransient<IEmailService, SendGridEmailService>();

    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

    // Add MVC services
    builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
        options.JsonSerializerOptions.MaxDepth = 500;
    });

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    else
    {
        Console.WriteLine("Running in Production Environment");
    }

    // Automatically apply migrations on startup
    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        dbContext.Database.Migrate(); // Applies pending migrations automatically
    }

    // Serve static files from wwwroot
    app.UseStaticFiles(new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider(
            Path.Combine(Directory.GetCurrentDirectory(), "agri_view", "build")),
        RequestPath = ""
    });

    // Enable routing
    app.UseRouting();

    app.UseHttpsRedirection();

    app.UseAuthorization();

    // Map controllers to the pipeline
    app.MapControllers();

    // Fallback to index.html for SPA routing (React)
    app.MapFallbackToFile("index.html", new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider(
            Path.Combine(Directory.GetCurrentDirectory(), "agri_view", "build"))
    });

    app.Use(async (context, next) =>
    {
        if (context.Request.Path.Value == "/favicon.ico")
        {
            context.Response.StatusCode = 204; // No content, tells the browser there's no favicon
            return;
        }
        await next();
    });


    app.Run();
}
catch (Exception ex)
{
    Console.WriteLine($"Application startup failed: {ex.Message}");
    throw;
}
