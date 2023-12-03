using Application.Extensions;
using Infrastructure.Extensions;
using Serilog;
using WebAPI.Extensions;
using WebAPI.Middlewares;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information() // Set the minimum log level
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.Seq(configuration["Seq:connectionString"])
    .CreateLogger();

builder.Services.AddLogging(c => c.AddSerilog(Log.Logger));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfrastructureDependencies();
builder.Services.AddApplicationDependency();
await builder.Services.AddPresentationDependencies(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<ExceptionHandingMiddleware>();

try
{
    Log.Information("Starting up");
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application start-up failed");
}
finally
{
    Log.CloseAndFlush();
}
