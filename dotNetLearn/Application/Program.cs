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

builder.Services.AddDbContextPool<RandDBContext>(o=>{
    string host = Environment.GetEnvironmentVariable("DB_HOST")??"host.docker.internal";
    if((Environment.GetEnvironmentVariable("INSTANCE_CONNECTION_NAME")??"")!="") host = (Environment.GetEnvironmentVariable("DB_SOCKET_PATH")??"/cloudsql")+"/"+Environment.GetEnvironmentVariable("INSTANCE_CONNECTION_NAME");
    string port = Environment.GetEnvironmentVariable("DB_PORT")??"5435";
    string db_name = Environment.GetEnvironmentVariable("DB_NAME")??"test_db";
    string user = Environment.GetEnvironmentVariable("DB_USER")??"root";
    string pass = Environment.GetEnvironmentVariable("DB_PASS")??"root";
    o.UseNpgsql("Host="+host+";Port="+port+";Username="+user+";Password="+pass+";Database="+db_name);
});

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
