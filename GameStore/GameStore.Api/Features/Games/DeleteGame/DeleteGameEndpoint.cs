using GameStore.Api.Data;
using GameStore.Api.Features.Games.Constants;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Features.Games.DeleteGame;

public static class DeleteGameEndpoint
{
    public static RouteHandlerBuilder MapDeleteGame(this IEndpointRouteBuilder app)
    {
        return app.MapDelete("/{id:guid}", async (Guid id, GameStoreContext dbContext) =>
            {
                await dbContext.Games.Where(game => game.Id == id).ExecuteDeleteAsync();

                return Results.NoContent();
            })
            .WithName(EndpointNames.DeleteGame);
        ;
    }
}