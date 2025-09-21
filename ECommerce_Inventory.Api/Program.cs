using System.Reflection;
using ECommerce_Inventory.Application.Mapping;
using ECommerce_Inventory.Application.Services;
using ECommerce_Inventory.Domain;
using ECommerce_Inventory.Domain.Identity;
using ECommerce_Inventory.Domain.Interfaces;
using ECommerce_Inventory.Domain.Repositories;
using ECommerce_Inventory.Infrastructure;
using ECommerce_Inventory.Infrastructure.Extensions;
using ECommerce_Inventory.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration)
    .CreateBootstrapLogger();

try
{
    Log.Information("Starting up the application");
    var builder = WebApplication.CreateBuilder(args);
    #region Serilog
    builder.Host.UseSerilog((ctx, lc) => lc
        .WriteTo.Console()
        .ReadFrom.Configuration(ctx.Configuration));
    #endregion

    #region AutoMapper
    builder.Services.AddAutoMapper(cfg => cfg.AddProfile<MapperProfile>(), Assembly.GetExecutingAssembly());
    #endregion

    builder.Services.AddScoped<IProductRepository, ProductRepository>();
    builder.Services.AddScoped<IProductService, ProductService>();
    builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
    builder.Services.AddScoped<ICategoryService, CategoryService>();
    builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
    builder.Services.AddScoped<ITokenService, TokenService>();

    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseSqlServer(
            builder.Configuration.GetConnectionString("ECommerceConn"),
            sqlOptions => sqlOptions.MigrationsAssembly("ECommerce_Inventory.Infrastructure")
        )
    );

    builder.Services.AddIdentity<Users, IdentityRole>(options =>
    {
        options.User.RequireUniqueEmail = true;
        options.Password.RequiredLength = 6;
        options.Password.RequireNonAlphanumeric = false;
    })
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

    builder.Services.AddJwtAuthentication(
        builder.Configuration["Jwt:Key"],
        builder.Configuration["Jwt:Issuer"],
        builder.Configuration["Jwt:Audience"]
    );

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddControllers();

    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.MapGet("/", () => Results.Redirect("/swagger"));

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    app.Run();
    Log.Information("Application started successfully");
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application start-up failed");
}
finally
{
    Log.CloseAndFlush();
}