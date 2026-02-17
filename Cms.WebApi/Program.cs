using Cms.Persistence.Models;
using Cms.WebApi.Configuration;
using CMS.Application;
using CMS.Application.Feature.Events.Handler;
using Lipip.Atomic.EntityFramework.Behaviors;
using MediatR;
using System;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddAtomicServices<ChurchMSDBContext>(
    builder.Configuration,
    "DefaultConnection",
    typeof(CreateEventHandler).Assembly);


var corsConfiguration = builder.Configuration.GetSection(nameof(CorsConfiguration)).Get<CorsConfiguration>();
if (corsConfiguration.CorsEnabled)
{
    builder.Services.AddCors(options =>
    {
        options.AddPolicy(corsConfiguration.PolicyName,
            policy =>
            {
                policy.WithOrigins(corsConfiguration.AllowedOrigins).AllowAnyHeader().AllowAnyMethod().WithExposedHeaders("Etag", "Cache-Control");
            });
    });
}

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCmsServices();


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

app.UseAuthorization();

app.MapControllers();

app.Run();
