using f00die_finder_be.Common;
using f00die_finder_be.Common.CurrentUserService;
using f00die_finder_be.Data;
using f00die_finder_be.Data.Seed;
using f00die_finder_be.Middlewares;
using f00die_finder_be.Repositories.UserRepository;
using f00die_finder_be.Services.AuthService;
using f00die_finder_be.Services.RestaurantService;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition(
        "Bearer",
        new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "Please enter a valid token",
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
            BearerFormat = "JWT",
            Scheme = "Bearer"
        }
    );

    options.AddSecurityRequirement(
        new OpenApiSecurityRequirement
        {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            Array.Empty<string>()
                        }
        }
    );
});

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
builder.Services.AddScoped<IRestaurantService, RestaurantService>();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
var app = builder.Build();

using var scope = app.Services.CreateScope();
var serviceProvider = scope.ServiceProvider;
try
{
    var context = serviceProvider.GetRequiredService<DataContext>();
    Seed.SeedAll(context);
    context.Database.Migrate();
}
catch (Exception ex)
{
    var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "Migration Failed");
}


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCustomExceptionHandler();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
