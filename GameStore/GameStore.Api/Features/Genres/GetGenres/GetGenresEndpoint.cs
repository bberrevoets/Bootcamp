using GameStore.Api.Data;
using GameStore.Api.Features.Games.Constants;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Features.Genres.GetGenres;

public static class GetGenresEndpoint
{
    public static void MapGetGenres(this IEndpointRouteBuilder app)
    {
        app.MapGet("/", async (GameStoreContext dbContext) =>
                await dbContext.Genres
                    .Select(genre => new GenreDto(genre.Id, genre.Name))
                    .AsNoTracking().ToListAsync())
            .WithName(EndpointNames.GetGenres);
    }
}