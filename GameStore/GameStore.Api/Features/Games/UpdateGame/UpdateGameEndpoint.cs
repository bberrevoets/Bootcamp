using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using GameStore.Api.Data;
using GameStore.Api.Features.Games.Constants;
using GameStore.Api.Shared.Authorization;
using GameStore.Api.Shared.FileUpload;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Api.Features.Games.UpdateGame;

public static class UpdateGameEndpoint
{
    public static RouteHandlerBuilder MapUpdateGame(this IEndpointRouteBuilder app)
    {
        return app.MapPut("/{id:guid}",
                async (Guid id, [FromForm] UpdateGameDto updatedGame, GameStoreContext dbContext,
                    FileUploader fileUploader, ClaimsPrincipal user) =>
                {
                    if (user.Identity?.IsAuthenticated == false)
                        return Results.Unauthorized();

                    var currentUserId = user.FindFirstValue(JwtRegisteredClaimNames.Sub);

                    if (string.IsNullOrEmpty(currentUserId)) return Results.Unauthorized();
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
                    existingGame.LastUpdatedBy = currentUserId;

                    await dbContext.SaveChangesAsync();

                    return Results.NoContent();
                })
            .WithParameterValidation()
            .DisableAntiforgery()
            .RequireAuthorization(Policies.AdminAccess);
        ;
    }
}