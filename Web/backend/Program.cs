using Microsoft.AspNetCore.Identity;
using Findgroup_Backend.Data;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Findgroup_Backend.Models;
using Findgroup_Backend.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Findgroup_Backend.Services;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Findgroup_Backend.Data.Seeders;
using Microsoft.AspNetCore.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
namespace Findgroup_Backend;
public class Program
{
    public static async Task Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
        builder.Configuration.AddJsonFile("secrets.json");
        IConfigurationSection jwtSettings = builder.Configuration.GetSection("JwtSettings");
        byte[] key = Encoding.UTF8.GetBytes(builder.Configuration["JwtSecret"]!);

        builder.Services.AddControllers();
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
        {
            string connectionString = builder.Configuration.GetConnectionString("DevelopmentDB")!;
            options.UseSqlite(connectionString).ConfigureWarnings(warnings =>
            warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
            options.EnableSensitiveDataLogging();
        });
        builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddCookie()
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new()
                {
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidAudience = jwtSettings["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });
        builder.Services.AddAuthorization();
        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy =>
            {
                policy.AllowAnyHeader()
                .AllowAnyOrigin()
                .AllowAnyMethod();
            });
        });
        
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IPostRepository, PostRepository>();
        builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
        builder.Services.AddScoped<ITokenRepository, TokenRepository>();
        builder.Services.AddScoped<ITokenService, TokenService>();
        builder.Services.AddScoped<IAuthService, AuthService>();
        builder.Services.AddAutoMapper(config =>
        {
            config.AddMaps(typeof(Program));
        });
        builder.Services.Configure<JsonOptions>(options =>
        {
            options.SerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
        });
        var app = builder.Build();
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        await RoleSeeder.SeedRolesAsync(roleManager);
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        app.UseHttpsRedirection();
        app.MapControllers();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseCors();
        app.Run();
    }
}
