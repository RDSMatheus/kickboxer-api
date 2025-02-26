using KickboxerApi.Models;
using KickboxerApi.Repository;
using KickboxerApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<KickboxerDatabaseSettings>(
    builder.Configuration.GetSection("KickboxerDatabase"));

builder.Services.AddSingleton<UsersService>();
builder.Services.AddSingleton<UsersRepository>();
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.MapOpenApi();
app.UseSwaggerUI((options) => options.SwaggerEndpoint("/openapi/v1.json", "Kickboxer API"));


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
