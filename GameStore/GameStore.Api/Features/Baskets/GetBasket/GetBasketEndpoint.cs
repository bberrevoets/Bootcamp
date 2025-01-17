﻿using GameStore.Api.Data;
using GameStore.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Features.Baskets.GetBasket;

public static class GetBasketEndpoint
{
    public static void MapGetBasket(this IEndpointRouteBuilder app)
    {
        app.MapGet("/{userId:guid}", async (Guid userId, GameStoreContext dbContext) =>
        {
            if (userId == Guid.Empty) return Results.BadRequest();

            var basket = await dbContext.Baskets
                .Include(basket => basket.Items)
                .ThenInclude(item => item.Game)
                .FirstOrDefaultAsync(basket => basket.Id == userId) ?? new CustomerBasket { Id = userId };

            var dto = new BasketDto(
                basket.Id,
                basket.Items.Select(item => new BasketItemDto(
                    item.GameId,
                    item.Game!.Name,
                    item.Game!.Price,
                    item.Quantity,
                    item.Game!.ImageUri)).OrderBy(item => item.Name));

            return Results.Ok(dto);
        });
    }
}