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
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Findgroup_Backend.Data.Repositories.Interfaces;
using Findgroup_Backend.Services.Interfaces;
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
        #region DBContext
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
        {
            string connectionString = builder.Configuration.GetConnectionString("DevelopmentDB")!;
            options.UseSqlite(connectionString).ConfigureWarnings(warnings =>
            warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
            options.EnableSensitiveDataLogging();
        });
        #endregion

        #region Identity Roles
        builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
        #endregion

        #region Authentication (JWT/Google)
        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
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
                options.Events = new JwtBearerEvents()
                {
                    OnMessageReceived = context =>
                    {
                        context.Token = context.Request.Cookies["accessToken"];
                        return Task.CompletedTask;
                    }
                };
            });
        builder.Services.AddAuthorization();
        #endregion

        #region Cors Policy
        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy =>
            {
                policy.AllowAnyHeader()
                .WithOrigins("http://localhost:5173")
                .AllowAnyMethod()
                .AllowCredentials();
            });
        });
        #endregion

        #region AutoMapper
        builder.Services.AddAutoMapper(config =>
        {
            config.AddMaps(typeof(Program));
        });
        #endregion

        #region Repositories
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IPostRepository, PostRepository>();
        builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
        builder.Services.AddScoped<ITokenRepository, TokenRepository>();
        builder.Services.AddScoped<ITokenService, TokenService>();
        builder.Services.AddScoped<IAuthService, AuthService>();
        builder.Services.AddScoped<IGroupRepository, GroupRepository>();
        #endregion


        builder.Services.Configure<JsonOptions>(options =>
        {
            options.SerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
        });
        var app = builder.Build();
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = services.GetRequiredService<UserManager<User>>();
        await AdminSeeder.SeedAdminAsync(userManager);
        await RoleSeeder.SeedRolesAsync(roleManager, userManager);
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
