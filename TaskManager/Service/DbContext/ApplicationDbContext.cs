namespace TaskManager.Service.DbContext;

using Microsoft.EntityFrameworkCore;
using Entities;

public class ApplicationDbContext : DbContext
{
    public DbSet<User> Users { get; set; }

    private readonly IConfiguration _configuration;

    public ApplicationDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_configuration.GetConnectionString("TaskManagerDataBase"));
    }
}