using FixWithCustomSerialization;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
IConfiguration _configuration = new ConfigurationBuilder()
                            .AddJsonFile("appsettings.json")
                             .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
                             .Build();
// Add services to the container.

builder.Services.AddControllers();
var config = new ServerConfig();
builder.Configuration.Bind(config);
var todoContext = new MongoService(config.MongoDB);
var repo = new DbService(todoContext);
builder.Services.AddSingleton<IDbService>(repo);
ConfigurationHelper.Initialize(_configuration);
builder.Services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();

