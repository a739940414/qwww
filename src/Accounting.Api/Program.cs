using Accounting.Infrastructure.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAccountingInfrastructure(builder.Configuration);

var jwtSection = builder.Configuration.GetSection("Jwt");
var secret = jwtSection.GetValue<string>("Secret") ?? "ChangeThisDevelopmentSecretKey123!";
var issuer = jwtSection.GetValue<string>("Issuer") ?? "AccountingSystem";
var audience = jwtSection.GetValue<string>("Audience") ?? "AccountingSystemClients";
var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));

builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,
            ValidIssuer = issuer,
            ValidAudience = audience,
            IssuerSigningKey = key
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

await app.Services.EnsureDatabaseMigratedAsync();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
