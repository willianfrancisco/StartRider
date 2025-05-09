using Application;
using DataMySql;
using DataMySql.Context;
using LoggerService;
using Microsoft.EntityFrameworkCore;
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