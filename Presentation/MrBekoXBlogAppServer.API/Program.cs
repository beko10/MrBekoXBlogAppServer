using MrBekoXBlogAppServer.API.Extensions;
using MrBekoXBlogAppServer.API.Middleware;
using MrBekoXBlogAppServer.Application.Extensions;
using MrBekoXBlogAppServer.Persistence.Extensions;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddExceptionHandling();
builder.Services.AddPersistanceServiceRegistration(builder.Configuration);
builder.Services.AddApplicationServices();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseExceptionHandling();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapGroup(prefix:"/api")
    .RegisterAllEndpoints();

app.Run();
