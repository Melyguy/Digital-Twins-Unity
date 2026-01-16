using DigitalTwinsBackend.Servers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}   

app.UseHttpsRedirection();
app.UseAuthorization();

// Minimal in-file server data so MapGet can return it.
var servers = new[]
{
    new Serverstate { id = "Server_01", Temperature = 75.5f, cpuUsage = 55.0f, Online = true },
    new Serverstate { id = "Server_02", Temperature = 80.0f, cpuUsage = 70.0f, Online = true },
    new Serverstate { id = "Server_03", Temperature = 90.0f, cpuUsage = 85.0f, Online = false },
    new Serverstate { id = "Server_04", Temperature = 65.0f, cpuUsage = 40.0f, Online = true },
    new Serverstate { id = "Server_05", Temperature = 78.0f, cpuUsage = 60.0f, Online = true },
    new Serverstate { id = "Server_06", Temperature = 88.0f, cpuUsage = 75.0f, Online = false },
};

app.MapGet("/api/servers", () => Results.Ok(servers));

app.MapControllers();
app.Run();
