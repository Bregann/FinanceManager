using FinanceManager.Domain;
using FinanceManagerAPI;
using Hangfire;
using Hangfire.Dashboard.BasicAuthorization;
using Hangfire.Dashboard.Dark;
using Hangfire.PostgreSql;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
GlobalConfiguration.Configuration.UsePostgreSqlStorage(c => c.UseNpgsqlConnection(Environment.GetEnvironmentVariable("FMApiConnString")));

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "allowUrls",
        policy =>
        {
            policy.WithOrigins("http://localhost:3000");
            policy.WithHeaders("Content-Type");
            policy.WithMethods("GET", "POST", "DELETE");
        });
});

Log.Logger = new LoggerConfiguration()
    .WriteTo.Async(x => x.File("/app/Logs/log.log", retainedFileCountLimit: 7, rollingInterval: RollingInterval.Day))
    .WriteTo.Console()
    .Enrich.WithProperty("Application", "FinanceManager-Api" + (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development" ? "-Test" : ""))
    .WriteTo.Seq("http://192.168.1.20:5341")
    .CreateLogger(); Log.Information("Logger Setup");

AppConfig.LoadConfig();
HangfireJobs.SetupHangfireJobs();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHangfire(configuration => configuration
        .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
        .UseSimpleAssemblyNameTypeSerializer()
        .UseRecommendedSerializerSettings()
        .UsePostgreSqlStorage(c => c.UseNpgsqlConnection(Environment.GetEnvironmentVariable("FMApiConnString")))
        .UseDarkDashboard()
        );

builder.Services.AddHangfireServer(options => options.SchedulePollingInterval = TimeSpan.FromSeconds(10));

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

var auth = new[] { new BasicAuthAuthorizationFilter(new BasicAuthAuthorizationFilterOptions
{
    RequireSsl = false,
    SslRedirect = false,
    LoginCaseSensitive = true,
    Users = new []
    {
        new BasicAuthAuthorizationUser
        {
            Login = AppConfig.HFUsername,
            PasswordClear = AppConfig.HFPassword
        }
    }
})};

app.MapHangfireDashboard("/hangfire", new DashboardOptions
{
    Authorization = auth
}, JobStorage.Current);

app.Run();