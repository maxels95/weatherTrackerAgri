using System.Reflection;
using AgriWeatherTracker.Data;
using AgriWeatherTracker.Service;
using Microsoft.EntityFrameworkCore;

try
{
    var builder = WebApplication.CreateBuilder(args);
    builder.Logging.AddConsole();

    // Add services to the container.
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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

    app.UseHttpsRedirection();

    app.UseAuthorization(); // Ensure authorization is added if you are using it

    // Map controllers to the pipeline
    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Console.WriteLine($"Application startup failed: {ex.Message}");
    throw;
}
