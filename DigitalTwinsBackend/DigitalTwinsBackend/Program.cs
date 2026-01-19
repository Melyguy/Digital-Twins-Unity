using DigitalTwinsBackend.Controllers;
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

//app.MapGet("/api/servers", () => Results.Ok(servers));

app.MapControllers();
app.Run();
