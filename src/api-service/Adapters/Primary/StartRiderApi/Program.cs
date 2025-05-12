using System.Text;
using Application;
using DataMySql;
using DataMySql.Context;
using Domain.Entities;
using LoggerService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RabbitMqConsumerService;
using RabbitMqPublishService;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    var xmlFilename = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFilename);
    opt.IncludeXmlComments(xmlPath);
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    opt.AddSecurityRequirement(new  OpenApiSecurityRequirement()
    {
    {
        new OpenApiSecurityScheme()
        {
            Reference = new  OpenApiReference()
            {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
            },
            Scheme = "oauth2",
            Name = "Bearer",
            In = ParameterLocation.Header,
        },
        new List<string>()
    }});
});
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));

var jwtSection = builder.Configuration.GetSection("Jwt");
var secretKey = jwtSection.GetValue<string>("SecretKey");
var key = Encoding.UTF8.GetBytes(secretKey);

builder.Services.AddAuthentication(a =>
{
    a.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    a.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(j =>
    {
      j.RequireHttpsMetadata = false;
      j.SaveToken = true;
      j.TokenValidationParameters = new TokenValidationParameters
      {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false
      };
    });

string connectionString = builder.Configuration.GetConnectionString("StartRiderDbConnectionString") ?? "server=172.17.0.2;database=StartRiderDb;user=root;password=123456;";

builder.Services.AddDbContext<StartRiderContext>(opt => 
    opt.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .CreateLogger();


// Adicionando dependencias dos serviços
builder.Services
    .AdicionaDependenciasInfraDataMysql()
    .AdicionaDependenciasInfraRabbitMqPublishService()
    .AdicionaDependenciasRabbitMqConsumerService()
    .AdicionaDependenciasApplications()
    .AdicionaDependenciaLoggerService();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();