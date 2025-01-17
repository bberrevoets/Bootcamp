using GameStore.Api.Data;
using GameStore.Api.Features.Games.Constants;
using GameStore.Api.Shared.FileUpload;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Api.Features.Games.UpdateGame;

public static class UpdateGameEndpoint
{
    public static RouteHandlerBuilder MapUpdateGame(this IEndpointRouteBuilder app)
    {
        return app.MapPut("/{id:guid}",
                async (Guid id, [FromForm] UpdateGameDto updatedGame, GameStoreContext dbContext,
                    FileUploader fileUploader) =>
                {
                    var existingGame = await dbContext.Games.FindAsync(id);

                    if (existingGame is null) return Results.NotFound();

                    if (updatedGame.ImageFile is not null)
                    {
                        var fileUploadResult =
                            await fileUploader.UploadFileAsync(updatedGame.ImageFile, StorageNames.GameImagesFolder);

                        if (!fileUploadResult.IsSuccess) return Results.BadRequest(fileUploadResult.ErrorMessage);
                        existingGame.ImageUri = fileUploadResult.FileUrl!;
                    }

                    existingGame.Name = updatedGame.Name;
                    existingGame.GenreId = updatedGame.GenreId;
                    existingGame.Price = updatedGame.Price;
                    existingGame.ReleaseDate = updatedGame.ReleaseDate;
                    existingGame.Description = updatedGame.Description;

                    await dbContext.SaveChangesAsync();

                    return Results.NoContent();
                }).WithParameterValidation()
            .WithName(EndpointNames.UpdateGame)
            .DisableAntiforgery();
    }
}