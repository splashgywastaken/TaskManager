using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;
using TaskManager.Service.Achievement;
using TaskManager.Service.DbContext;
using TaskManager.Service.User;

var builder = WebApplication.CreateBuilder(args);

// add services to DI container
{
    var services = builder.Services;
    var env = builder.Environment;
    
    // Setting up JWT-based authorization and authentication
    services.AddAuthorization();
    services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = AuthOptions.ISUSER,
                ValidateAudience = true,
                ValidAudience = AuthOptions.AUDIENCE,
                ValidateLifetime = true,
                IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                ValidateIssuerSigningKey = true
            };
        });
    // Setting up DB context
    services.AddDbContext<ApplicationDbContext>();
    services.AddCors();
    services.AddControllers().AddJsonOptions(x =>
    {
        x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());

        x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });
    services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

    // Configue DI for application services
    services.AddScoped<IUserService, UserService>();
    services.AddScoped<IAchievementService, AchievementService>();
}

var app = builder.Build();

// Configure HTTP request pipeline
{
    //
    app.UseCors(x => x
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader()
    );

    app.MapControllers();
    app.Map("/data", [Authorize] (HttpContext context) => $"Hello World!");
}

// File system
app.UseDefaultFiles(
    new DefaultFilesOptions
    {
        DefaultFileNames = new List<string>() {"index.html"}
    });
app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.Run();

public static class AuthOptions
{
    public const string ISUSER = "MyAuthServer";
    public const string AUDIENCE = "MyAuthClient"; // потребитель токена
    const string KEY = "mysupersecret_secretkey!123";   // ключ для шифрации
    public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
}