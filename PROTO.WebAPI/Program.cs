using Microsoft.OpenApi.Models;
using PROTO.UseCase.Interfaces;
using PROTO.Core.Models;
using PROTO.Infrastructure.Repositories;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
    {
    c.EnableAnnotations();

        c.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "PROTO.WebAPI",
            Version = "v1",
            Description = "Prototype Demo of Dapper and SQL Server using Swagger",
            Contact = new OpenApiContact
            {
                Name = "Test Contact",
                Email = String.Empty,
                Url = new Uri("https://jira.test.com/contact")
            },
            License = new OpenApiLicense
            {
                Name = "Test License",
                Url = new Uri("https://test.com/license") // Add url of license details
            }
        });
    });

    builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
    builder.Services.AddTransient<IHostDeviceRepository, HostDeviceRepository>();
    builder.Services.AddTransient<IUnitOfWorkSP, UnitOfWorkSP>();
    builder.Services.AddTransient<IHostDeviceRepositorySP, HostDeviceRepositorySP>();
    var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
