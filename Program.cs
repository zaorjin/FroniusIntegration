using FroniusIntegration;
using FroniusIntegration.Extensions;
using Hangfire;
using Hangfire.SqlServer;

WebApplicationOptions options = new()
{
  ContentRootPath = AppContext.BaseDirectory,
  Args = args
};

var builder = WebApplication.CreateBuilder(options);

builder.Host.UseWindowsService();

// Add options.
builder.Services.AddOptionsConfiguration(builder.Configuration);

// Add services to the container.
builder.Services.AddControllers();

// Add Handlers
builder.Services.AddCustomHandlers();

// Add Repositories
builder.Services.AddCustomRepositories();

// Add Services
builder.Services.AddCustomServices();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Controller Service
builder.Services.AddTransient<ControllerService, ControllerService>();
// Add AppDataContext
builder.Services.AddScoped<AppDataContext, AppDataContext>();

builder.Services.AddTransient<FroniusRestClient, FroniusRestClient>();
builder.Services.AddTransient<FroniusRestClientDynamic, FroniusRestClientDynamic>();

var connectionString = builder.Configuration.GetConnectionString("Hangfire");

builder.Services.AddHangfire(configuration =>
{
  configuration.UseSqlServerStorage(
    connectionString,
    new SqlServerStorageOptions
    {
      CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
      SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
      QueuePollInterval = TimeSpan.Zero,
      UseRecommendedIsolationLevel = true
    });
});

builder.Services.AddHangfireServer();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}
else
{
  builder.WebHost.UseUrls($"http://*:5066");
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseHangfireDashboard();

const string oneHourCronExpression = "00 0/1 * * *";
RecurringJob.AddOrUpdate<ControllerService>(service => service!.EnergyHoursJobAsync(), oneHourCronExpression);
app.Run();