using AutoMapper;
using EcommerceAPI;
using EcommerceAPI.Data;
using EcommerceAPI.Models.AuthModels;
using EcommerceAPI.Service;
using EcommerceAPI.Service.IService;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MySQL.Data.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(option =>
{
    var connectionString = builder.Configuration.GetConnectionString("Default Connection");
    option.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString) );
});

IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("Apisettings:JwtOptions"));
builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

ApplyPendingMigrations();

app.Run();

void ApplyPendingMigrations()
{
    using (var scope = app.Services.CreateScope())
    {
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        if (db.Database.GetPendingMigrations().Count() > 0)
        {
          db.Database.Migrate();  
        }
    }
}