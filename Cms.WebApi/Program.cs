using Cms.Persistence.Models;
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
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
