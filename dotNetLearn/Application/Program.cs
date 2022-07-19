using dotNetLearn.Services;
using Microsoft.EntityFrameworkCore;
using Google.Cloud.Diagnostics.AspNetCore3;
using Google.Cloud.Diagnostics.Common;

var builder = WebApplication.CreateBuilder(args);

if((Environment.GetEnvironmentVariable("PORT")??"")!="") builder.WebHost.UseUrls("http://+:"+Environment.GetEnvironmentVariable("PORT"));


// Add services to the container.
var toLog = "";
if((Environment.GetEnvironmentVariable("APPLICATIONINSIGHTS_CONNECTION_STRING")??"")!="") { //Azure
    toLog+="Got connection string.\n";
    builder.Services.AddApplicationInsightsTelemetry(Environment.GetEnvironmentVariable("APPLICATIONINSIGHTS_CONNECTION_STRING"));
} else if((Environment.GetEnvironmentVariable("PORT")??"")!=""){ //GCP
    // Explicitly create trace options that will propagate any exceptions thrown during tracing.
    RetryOptions retryOptions = RetryOptions.NoRetry(ExceptionHandling.Propagate);
    // Also set the no buffer option so that tracing is attempted immediately.
    BufferOptions bufferOptions = BufferOptions.NoBuffer();
    TraceOptions traceOptions = TraceOptions.Create(bufferOptions: bufferOptions, retryOptions: retryOptions);

    builder.Services.AddGoogleDiagnosticsForAspNetCore(new AspNetCoreTraceOptions{
        ServiceOptions = new TraceServiceOptions {
            Options = traceOptions
        }
    });
    //builder.Services.AddGoogleTraceForAspNetCore();
    //builder.WebHost.ConfigureLogging(builder => builder.AddGoogle(new LoggingServiceOptions {}));
}

builder.Services.AddCors(options =>
{
    options.AddPolicy("frontend",
        policy =>
        {
            policy.WithOrigins("http://localhost:4173","http://localhost:3000",Environment.GetEnvironmentVariable("FRONTEND_URL")??"*")
                                .AllowAnyHeader()
                                .AllowAnyMethod();
        });
});

builder.Services.AddControllers();

builder.Services.AddDbContextPool<RandDBContext>(DBConnect.prepare);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

var logger = app.Services.GetRequiredService<ILogger<Program>>();
logger.LogInformation("Host created.");
logger.LogInformation(toLog);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if((Environment.GetEnvironmentVariable("ASPNETCORE_Kestrel__Certificates__Default__Path")??"")!="")
app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
