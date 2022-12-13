global using TaskManager.Entities;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.OpenApi.Models;
using TaskManager.Service.Data.DbContext;
using TaskManager.Service.Data.Mapping;
using TaskManager.Service.Entities.Achievement;
using TaskManager.Service.Entities.Project;
using TaskManager.Service.Entities.Tag;
using TaskManager.Service.Entities.Task;
using TaskManager.Service.Entities.User;

var builder = WebApplication.CreateBuilder(args);

// add services to DI container
{
    var services = builder.Services;
    var env = builder.Environment;
    
    // Setting up JWT-based authorization and authentication
    services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(options =>
        {
            options.LoginPath = "/user/login";
            options.LogoutPath = "/user/logout";
        });
    services.AddAuthorization();

    ConfigureApplicationServices(services);

    // Setting up mapping
    ConfigureMapping(services);

    // Setting up DB context
    ConfigureDbContext(services);

    // Configue DI for application services
    ConfigureApplicationDataServices(services);
}

var app = builder.Build();

// Swagger configue
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/V0.1/swagger.json", "TaskManagerAPI V0.1");
    c.RoutePrefix = "";
});

// Configure HTTP request pipeline
{
    //
    app.UseCors(x => x
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader()
    );

    app.MapControllers();
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

static void ConfigureMapping(IServiceCollection services)
{
    services.AddAutoMapper(typeof(AppMappingProfile));
}

static void ConfigureDbContext(IServiceCollection services)
{
    services.AddDbContext<TaskManagerDBContext>();
    services.AddCors();
    services.AddControllers().AddJsonOptions(x =>
    {
        x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());

        x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });
    services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
}

static void ConfigureApplicationServices(IServiceCollection services)
{
    services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("V0.1", new OpenApiInfo {Title = "TaskManagerAPI", Version = "V0.1"});
    });
}

static void ConfigureApplicationDataServices(IServiceCollection services)
{
    services.AddScoped<IUserService, UserService>();
    services.AddScoped<IAchievementService, AchievementService>();
    services.AddScoped<ITagService, TagService>();
    services.AddScoped<ITaskService, TaskService>();
    services.AddScoped<IProjectService, ProjectService>();
}

public static class AuthOptions
{
    public const string ISUSER = "MyAuthServer";
    public const string AUDIENCE = "MyAuthClient"; // потребитель токена
    const string KEY = "mysupersecret_secretkey!123";   // ключ для шифрации
    public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
}