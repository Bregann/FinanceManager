using FinanceManagerAPI;
using FinanceManagerAPI.Data.MonzoApi;
using FinanceManagerAPI.Services;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "allowUrls",
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:3000", "https://fmdemo.bregan.me");
                          policy.WithHeaders("Content-Type");
                          policy.WithMethods("GET", "POST", "DELETE");
                      });
});

Log.Logger = new LoggerConfiguration().WriteTo.Async(x => x.File("Logs/log.log", retainedFileCountLimit: null, rollingInterval: RollingInterval.Day)).WriteTo.Console().CreateLogger();
Log.Information("Logger Setup");

AppConfig.LoadConfigVariables();

await JobScheduler.SetupJobScheduler();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.WebHost.UseUrls("http://localhost:5003");
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("allowUrls");

app.UseAuthorization();

app.MapControllers();

app.Run();