using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// Keycloak (Reference: https://github.com/iulianoana/jwt-dotnetcore-web)
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
        {
            options.Authority = "http://localhost:8080/auth/realms/SSO";
            options.Audience = "product-api";
            options.IncludeErrorDetails = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = true,
                ValidIssuer = "http://localhost:8080/auth/realms/SSO",
                ValidateLifetime = true
            };
            options.RequireHttpsMetadata = false;
        }
    );

builder.Services.AddAuthorization();

var app = builder.Build();

app.MapControllers();
app.UseAuthentication();
app.UseAuthorization();


app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true) // allow any origin
    .AllowCredentials()
);

app.Run();
