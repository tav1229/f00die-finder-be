using f00die_finder_be.Common;
using f00die_finder_be.Data;
using f00die_finder_be.Data.Seed;
using f00die_finder_be.Middlewares;
using f00die_finder_be.Services.UserService;
using f00die_finder_be.Services.AuthService;
using f00die_finder_be.Services.RestaurantService;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using f00die_finder_be.Services.Location;
using f00die_finder_be.Services.ServingTypeService;
using f00die_finder_be.Services.AdditionalServiceService;
using f00die_finder_be.Services.CuisineTypeService;
using f00die_finder_be.Services.ReservationService;
using f00die_finder_be.Services.ReviewComment;
using f00die_finder_be.Common.CurrentUserService;
using f00die_finder_be.Data.UnitOfWork;
using f00die_finder_be.Common.FileService;
using f00die_finder_be.Common.CacheService;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

if (!builder.Environment.IsDevelopment())
{
    builder.Configuration["Secret"] = Environment.GetEnvironmentVariable("Secret");
    builder.Configuration["DefaultConnection"] = Environment.GetEnvironmentVariable("DefaultConnection");
    builder.Configuration["Redis"] = Environment.GetEnvironmentVariable("Redis");
}

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

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
builder.Services.AddScoped<IRestaurantService, RestaurantService>();
builder.Services.AddScoped<ILocationService, LocationService>();
builder.Services.AddScoped<IServingTypeService, ServingTypeService>();
builder.Services.AddScoped<ICuisineTypeService, CuisineTypeService>();
builder.Services.AddScoped<IAdditionalServiceService, AdditionalServiceService>();
builder.Services.AddScoped<IServingTypeService, ServingTypeService>();
builder.Services.AddScoped<IReservationService, ReservationService>();
builder.Services.AddScoped<IReviewCommentService, ReviewCommentService>();
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<ICacheService, CacheService>();

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
builder.Services.AddHttpContextAccessor();
builder.Services.AddMemoryCache();
var redisConnectionString = builder.Configuration.GetConnectionString("Redis");
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = redisConnectionString;
    options.InstanceName = "f00die_finder_be";
});
var redis = ConnectionMultiplexer.Connect(redisConnectionString);
redis.GetDatabase().Execute("FLUSHDB");

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


app.UseSwagger();
app.UseSwaggerUI();

app.UseCustomExceptionHandler();

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
