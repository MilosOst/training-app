using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TrainingApp.Features.Authentication.Models;
using TrainingApp.Features.Trainings;
using TrainingApp.Features.Trainings.FixedDrills;
using TrainingApp.Features.Trainings.UserTrainingDrills;
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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserEntityTypeConfiguration).Assembly);
        
        var converter = new EnumToStringConverter<AgeGroup>();

        modelBuilder
            .Entity<UserTraining>()
            .Property(e => e.Age)
            .HasConversion(converter);
        
        var converter2 = new EnumToStringConverter<Category>();

        modelBuilder
            .Entity<FixedDrill>()
            .Property(e => e.Category)
            .HasConversion(converter2);
        
    }
    

    public DbSet<User> Users { get; set; }
    public DbSet<UserSession> UserSessions { get; set; }
    
    public DbSet<FixedDrill> FixedDrills { get; set; }
    
    public DbSet<UserTrainingDrill> UserTrainingDrills { get; set; }
    
    public DbSet<UserTraining> UserTrainings { get; set; }
}