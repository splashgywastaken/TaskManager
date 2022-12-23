global using TaskManager.Entities;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using TaskManager.Service.Data.DbContext;
using TaskManager.Service.Data.Mapping;
using TaskManager.Service.Entities.Achievement;
using TaskManager.Service.Entities.Project;
using TaskManager.Service.Entities.Tag;
using TaskManager.Service.Entities.Task;
using TaskManager.Service.Entities.TaskGroup;
using TaskManager.Service.Entities.User;

var builder = WebApplication.CreateBuilder(args);

// add services to DI container
{
    var services = builder.Services;
    var env = builder.Environment;
    
    ConfigureApplicationServices(services, env);

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
    app.MapControllers();
    app.UseCors();

    // Index page endpoint
    app.Map("/adminPanel", async (context) =>
    {
        context.Response.ContentType = "text/html; charset=utf-8";
        await context.Response.SendFileAsync("wwwroot/adminPanel.html");
    });
}

// File system
app.UseDefaultFiles(
    new DefaultFilesOptions
    {
        DefaultFileNames = new List<string>() {"index.html"}
    });
app.UseStaticFiles();

app.UseCookiePolicy();
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
    services.AddControllers().AddJsonOptions(x =>
    {
        x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());

        x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });
    services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
}

static void ConfigureApplicationServices(IServiceCollection services, IWebHostEnvironment env)
{
    // Setting up Cookie-based authorization and authentication
    services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
        {
            options.Cookie.HttpOnly = false;
            options.Cookie.SecurePolicy = env.IsDevelopment() ? CookieSecurePolicy.None : CookieSecurePolicy.Always;
            options.Cookie.Name = CookieAuthenticationDefaults.AuthenticationScheme;
            options.LoginPath = "/user/login";
            options.LogoutPath = "/user/logout";
        });

    var cookieOptions = new CookiePolicyOptions
    {
    };

    services.Configure<CookiePolicyOptions>(options =>
    {
        options.Secure = CookieSecurePolicy.SameAsRequest;
        options.HttpOnly = HttpOnlyPolicy.None;
        options.CheckConsentNeeded = (context) => false;
    });
    services.AddAuthorization();

    services.AddCors(options =>
    {
        options.AddDefaultPolicy(builder =>
        {
            builder
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
    });

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
    services.AddScoped<ITaskGroupService, TaskGroupService>();
}