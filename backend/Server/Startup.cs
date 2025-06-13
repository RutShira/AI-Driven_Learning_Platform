using BL;
using BL.Api;
using BL.Models;
using Dal;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Security.Claims;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

public class Startup
{
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        var jwtSection = _configuration.GetSection("Jwt");
        var key = Encoding.UTF8.GetBytes(jwtSection["Key"]);

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSection["Issuer"],
                    ValidAudience = jwtSection["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });

        services.AddAuthorization(options =>
        {
            options.AddPolicy("AdminOnly", policy => policy.RequireClaim(ClaimTypes.Role, "Admin"));
        });


        services.AddControllers();
        services.Configure<OpenAiSettings>(_configuration.GetSection("OpenAiSettings"));
        


        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
        });
        
        string connectionString = _configuration.GetConnectionString("DefaultConnection");
        services.AddScoped<IBL>(provider =>
        new BLManager(connectionString, provider.GetRequiredService<IOptions<OpenAiSettings>>()));

        
        

        services.AddCors(options =>
        {
            options.AddPolicy("AllowSpecificOrigin",
                builder => builder.WithOrigins("http://localhost:5173")
                                  .AllowAnyMethod()
                                  .AllowAnyHeader());
        });
    }

    public void Configure(IApplicationBuilder app)
    {
      
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"));
       
        app.UseMiddleware<ErrorHandlingMiddleware>();

        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseCors("AllowSpecificOrigin");
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
