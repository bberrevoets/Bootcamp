using GameStore.Api.Data;

namespace GameStore.Api.Features.Games.GetGame;

public static class GetNameEndpoint
{
    public static RouteHandlerBuilder MapGetGame(this IEndpointRouteBuilder app)
    {
        return app.MapGet("/{id:guid}", async (Guid id, GameStoreContext dbContext) =>
        {
            var game = await dbContext.Games.FindAsync(id);

            return game is null
                ? Results.NotFound()
                : Results.Ok(new GameDetailsDto(game.Id, game.Name, game.GenreId, game.Price, game.ReleaseDate,
                    game.Description, game.ImageUri, game.LastUpdatedBy));
        });
    }
}