using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using ProductApi.Authorization;
using Microsoft.AspNetCore.Authorization;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();

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

builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy("product#example", policy =>
            policy.Requirements.Add(
                new ResourceScopeRequirement("Product", "example")
            )
        );
    });


// Custom Claims Transformer for Keycloak Roles
builder.Services.AddTransient<IClaimsTransformation, ClaimsTransformer>();

// Custom IAuthorizationHandler for acquiring the uma-ticket 
builder.Services.AddHttpClient<IAuthorizationHandler, ResourceScopeHandler>();

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