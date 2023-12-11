using FinanceManagerAPI;
using Hangfire;
using Hangfire.Dashboard.BasicAuthorization;
using Hangfire.Dashboard.Dark;
using Hangfire.PostgreSql;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
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

Log.Logger = new LoggerConfiguration().WriteTo.Async(x => x.File("Logs/log.log", retainedFileCountLimit: 7, rollingInterval: RollingInterval.Day)).WriteTo.Console().CreateLogger();
Log.Information("Logger Setup");

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHangfire(configuration => configuration
        .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
        .UseSimpleAssemblyNameTypeSerializer()
        .UseRecommendedSerializerSettings()
        .UsePostgreSqlStorage(c => c.UseNpgsqlConnection(Environment.GetEnvironmentVariable("BTBConnString")))
        .UseDarkDashboard()
        );

builder.Services.AddHangfireServer(options => options.SchedulePollingInterval = TimeSpan.FromSeconds(10));

#if RELEASE
builder.WebHost.UseUrls("http://localhost:5003");
#endif

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