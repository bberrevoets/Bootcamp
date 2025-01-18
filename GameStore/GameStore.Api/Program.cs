using GameStore.Api.Data;
using GameStore.Api.Features.Baskets;
using GameStore.Api.Features.Games;
using GameStore.Api.Features.Genres;
using GameStore.Api.Shared.Authorization;
using GameStore.Api.Shared.ErrorHandling;
using GameStore.Api.Shared.FileUpload;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

var connString = builder.Configuration.GetConnectionString("GameStore");
builder.Services.AddSqlite<GameStoreContext>(connString);

builder.Services.AddHttpLogging(options =>
{
    options.LoggingFields = HttpLoggingFields.RequestMethod |
                            HttpLoggingFields.RequestPath |
                            HttpLoggingFields.ResponseStatusCode |
                            HttpLoggingFields.Duration;
    options.CombineLogs = true;
});

// Add Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "GameStore API",
        Version = "v1",
        Description = "API documentation for the GameStore project.",
        Contact = new OpenApiContact
        {
            Name = "Bert Berrevoets",
            Email = "bert@berrevoets.net"
        }
    });
});

builder.Services.AddHttpContextAccessor()
    .AddSingleton<FileUploader>();

builder.Services.AddAuthentication()
    .AddJwtBearer(options =>
    {
        options.MapInboundClaims = false;
        options.TokenValidationParameters.RoleClaimType = "role";
    });

builder.AddGameStoreAuthorization();

var app = builder.Build();

app.UseStaticFiles();

app.MapGames();
app.MapGenres();
app.MapBaskets();

app.UseHttpLogging();

// Add Swagger middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "GameStore API V1");
        options.RoutePrefix = string.Empty;
        options.DefaultModelsExpandDepth(-1);
    });
}

if (!app.Environment.IsDevelopment()) app.UseExceptionHandler();

app.UseStatusCodePages();

await app.InitializeDbAsync();

app.Run();