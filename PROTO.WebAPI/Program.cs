using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using PROTO.UseCase.Interfaces;
using PROTO.Core.Models;
using PROTO.Infrastructure.Repositories;
using PROTO.WebAPI.Handlers;
using Microsoft.Extensions.Options;

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
        c.AddSecurityDefinition("basic", new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            Scheme = "basic",
            In = ParameterLocation.Header,
            Description = "Basic Authorization header using the Bearer scheme."
        });

        c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
              new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "basic"
                    }
                },
                new string[] {}
        }
    });
    });
    builder.Services.AddAuthentication("BasicAuthentication").AddScheme<AuthenticationSchemeOptions,BasicAuthenticationHandler>("BasicAuthentication", null);
    builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
    builder.Services.AddTransient<IHostDeviceRepository, HostDeviceRepository>();
    builder.Services.AddTransient<IUnitOfWorkSP, UnitOfWorkSP>();
    builder.Services.AddTransient<IHostDeviceRepositorySP, HostDeviceRepositorySP>();
    builder.Services.AddTransient<IBasicAuthRepository, BasicAuthRepository>();
    builder.Services.AddTransient<IUnitOfWorkAuth, UnitOfWorkAuth>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
