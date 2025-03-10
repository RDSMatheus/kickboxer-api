using System.Text;
using KickboxerApi.Models;
using KickboxerApi.Repository;
using KickboxerApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

var jwtKey = builder.Configuration["Jwt:Key"] ?? throw new ArgumentNullException("Jwt:Key", "JWT Key cannot be null");


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false; //Em produção é necessario colocar true, para https
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true
    };
});

builder.Services.AddAuthorization();

builder.Services.Configure<KickboxerDatabaseSettings>(
    builder.Configuration.GetSection("KickboxerDatabase"));

builder.Services.AddSingleton<UsersService>();
builder.Services.AddSingleton<UsersRepository>();
builder.Services.AddSingleton<VideoServices>();
builder.Services.AddSingleton<VideoRepository>();
builder.Services.AddSingleton<CloudinaryServices>();

builder.Services.AddControllers();

builder.Services.AddOpenApi();

var app = builder.Build();


app.MapOpenApi();
app.UseSwaggerUI((options) => options.SwaggerEndpoint("/openapi/v1.json", "Kickboxer API"));


app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
