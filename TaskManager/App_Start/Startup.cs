using System.Configuration;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using TaskManager.Service.DbContext;
using TaskManager.Service.User;

var builder = WebApplication.CreateBuilder(args);

// add services to DI container
{
    var services = builder.Services;
    var env = builder.Environment;
    
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
}

app.UseDefaultFiles(
    new DefaultFilesOptions
    {
        DefaultFileNames = new List<string>() {"index.html"}
    });

app.Run();