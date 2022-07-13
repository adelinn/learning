using dotNetLearn.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

if((Environment.GetEnvironmentVariable("PORT")??"")!="") builder.WebHost.UseUrls("http://+:"+Environment.GetEnvironmentVariable("PORT"));
// Add services to the container.
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
