using Microsoft.EntityFrameworkCore;
using TournamentApp.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Add DbContext
builder.Services.AddDbContext<TournamentDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add DI Services
builder.Services.AddScoped(typeof(TournamentApp.Domain.Interfaces.IGenericRepository<>), typeof(TournamentApp.Infrastructure.Repositories.GenericRepository<>));
builder.Services.AddScoped<TournamentApp.Application.Interfaces.ITournamentService, TournamentApp.Application.Services.TournamentService>();
builder.Services.AddScoped<TournamentApp.Application.Interfaces.ITeamService, TournamentApp.Application.Services.TeamService>();
builder.Services.AddScoped<TournamentApp.Application.Interfaces.IMatchService, TournamentApp.Application.Services.MatchService>();
builder.Services.AddScoped<TournamentApp.Application.Interfaces.IAuthService, TournamentApp.Application.Services.AuthService>();

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

// Add services to the container.
builder.Services.AddControllers();

// Add JWT Authentication
builder.Services.AddAuthentication(Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
        };
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Automaticaly Seed Admin User on Startup
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<TournamentDbContext>();
    context.Database.EnsureCreated(); // Asegura que la DB exista
    
    var adminUser = context.Users.FirstOrDefault(u => u.Email == "admin@tournament.com");
    if (adminUser == null)
    {
        context.Users.Add(new TournamentApp.Domain.Entities.User
        {
            Id = Guid.NewGuid(),
            Username = "Admin",
            Email = "admin@tournament.com",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123"),
            Role = TournamentApp.Domain.Enums.Role.Admin
        });
        context.SaveChanges();
    }
    else
    {
        // Fuerza la contraseña por si la cambiaste
        adminUser.PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123");
        adminUser.Role = TournamentApp.Domain.Enums.Role.Admin;
        context.SaveChanges();
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection(); // <-- Comentado para evitar errores de Red/CORS en local
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
