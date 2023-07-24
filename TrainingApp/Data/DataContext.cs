using Microsoft.EntityFrameworkCore;
using TrainingApp.Features.Users;

namespace TrainingApp.Data;

public class DataContext: DbContext
{
    private readonly IConfiguration _configuration;

    public DataContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(Environment.GetEnvironmentVariable("CONNECTION_STRING"));
    }
    
    public DbSet<User> Users { get; set; }
}