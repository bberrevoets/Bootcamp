using System.Security.Claims;
using GameStore.Api.Data;
using GameStore.Api.Features.Games.Constants;
using GameStore.Api.Models;
using GameStore.Api.Shared.FileUpload;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Api.Features.Games.CreateGame;

public static class CreateGameEndpoint
{
    private const string DefaultImageUri = "https://placehold.co/100";

    public static RouteHandlerBuilder MapCreateGame(this IEndpointRouteBuilder app)
    {
        return app.MapPost("/",
                async ([FromForm] CreateGameDto createGameDto, GameStoreContext dbContext, ILogger<Program> logger,
                    FileUploader fileUploader, ClaimsPrincipal user) =>
                {
                    if (user.Identity?.IsAuthenticated == false)
                        return Results.Unauthorized();

                    var imageUri = DefaultImageUri;

                    if (createGameDto.ImageFile is not null)
                    {
                        var fileUploadResult =
                            await fileUploader.UploadFileAsync(createGameDto.ImageFile, StorageNames.GameImagesFolder);
                        if (!fileUploadResult.IsSuccess)
                            return Results.BadRequest(new { message = fileUploadResult.ErrorMessage });

                        imageUri = fileUploadResult.FileUrl;
                    }

                    var game = new Game
                    {
                        Name = createGameDto.Name,
                        GenreId = createGameDto.GenreId,
                        Price = createGameDto.Price,
                        ReleaseDate = createGameDto.ReleaseDate,
                        Description = createGameDto.Description,
                        ImageUri = imageUri!
                    };

                    dbContext.Games.Add(game);

                    await dbContext.SaveChangesAsync();

                    logger.LogInformation("Created game {GameName} with price {GamePrice}", game.Name, game.Price);

                    return Results.CreatedAtRoute(EndpointNames.GetGame, new { id = game.Id },
                        new GameDetailsDto(game.Id, game.Name, game.GenreId, game.Price, game.ReleaseDate,
                            game.Description, game.ImageUri));
                })
            .WithParameterValidation()
            .WithName(EndpointNames.CreateGame)
            .DisableAntiforgery();
    }
}