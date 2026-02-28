using Cms.Persistence.Models;
using Cms.WebApi.Configuration;
using CMS.Application;
using CMS.Application.Feature.Masterlists.Events.Handler;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddAtomicServices<ChurchMSDBContext>(
    builder.Configuration,
    "DefaultConnection",
    typeof(CreateEventHandler).Assembly);

var jwtSettings = builder.Configuration.GetSection("Jwt");
var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]!);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

var corsConfiguration = builder.Configuration.GetSection(nameof(CorsConfiguration)).Get<CorsConfiguration>();
if (corsConfiguration.CorsEnabled)
{
    builder.Services.AddCors(options =>
    {
        options.AddPolicy(corsConfiguration.PolicyName,
            policy =>
            {
                policy.WithOrigins(corsConfiguration.AllowedOrigins)
                .AllowAnyHeader()
                .AllowAnyMethod()
                .WithExposedHeaders("Etag", "Cache-Control");
            });
    });
}

builder.Services.AddControllers();

builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = new Microsoft.AspNetCore.Authorization.AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCmsServices();

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter token like: Bearer {your token}"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            new string[] {}
        }
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

if (corsConfiguration.CorsEnabled)
{
    app.UseCors(corsConfiguration.PolicyName);
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
