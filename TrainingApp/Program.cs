using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using TrainingApp.Data;
using TrainingApp.Features.Authentication;
using TrainingApp.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

DotNetEnv.Env.Load();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddLogging();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "sessionId";
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        options.Cookie.SameSite = SameSiteMode.Strict;
        options.EventsType = typeof(CookieSessionValidationMiddleware);

    });

builder.Services.AddScoped<CookieSessionValidationMiddleware>();
builder.Services.AddTransient<GlobalExceptionMiddlewareHandler>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

builder.Services.AddEntityFrameworkNpgsql().AddDbContext<DataContext>(opt
    => opt.UseNpgsql(Environment.GetEnvironmentVariable("CONNECTION_STRING")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<GlobalExceptionMiddlewareHandler>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();