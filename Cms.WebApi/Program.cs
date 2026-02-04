using Cms.Persistence.Models;
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

app.UseAuthorization();

app.MapControllers();

app.Run();
