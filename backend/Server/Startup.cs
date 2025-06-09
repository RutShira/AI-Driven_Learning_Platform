using BL;
using BL.Api;
using Dal;
using Microsoft.OpenApi.Models;

public class Startup
{
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        // Add services to the container.
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        // Register DalManager with a connection string from configuration
        string connectionString = _configuration.GetConnectionString("DefaultConnection");
        services.AddScoped<IBL>(bl => new BLManager(connectionString));
        // הוספת הגדרת CORS
        services.AddCors(options =>
        {
            options.AddPolicy("AllowSpecificOrigin",

                builder => builder.WithOrigins("http://localhost:5173") // החלף בכתובת המתאימה

                                  .AllowAnyMethod()
                                  .AllowAnyHeader());
        });
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
        });

    }
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"));
        }
        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}