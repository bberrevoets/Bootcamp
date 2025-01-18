﻿using System.Security.Claims;
using GameStore.Api.Data;
using GameStore.Api.Features.Baskets.Authorization;
using GameStore.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Features.Baskets.UpsertBasket;

public static class UpsertBasketEndpoint
{
    public static void MapUpsertBasket(this IEndpointRouteBuilder app)
    {
        // PUT /baskets/B98502E4-EC5E-4B23-8A56-A7B2BB241745
        app.MapPut("/{userId:guid}", async (
            Guid userId,
            UpsertBasketDto upsertBasketDto,
            GameStoreContext dbContext,
            IAuthorizationService authorizationService,
            ClaimsPrincipal user
        ) =>
        {
            var basket = await dbContext.Baskets
                .Include(basket => basket.Items)
                .FirstOrDefaultAsync(basket => basket.Id == userId);

            if (basket is null)
            {
                basket = new CustomerBasket
                {
                    Id = userId,
                    Items = upsertBasketDto.Items.Select(item => new BasketItem
                    {
                        GameId = item.Id,
                        Quantity = item.Quantity
                    }).ToList()
                };

                dbContext.Baskets.Add(basket);
            }
            else
            {
                basket.Items = upsertBasketDto.Items.Select(item => new BasketItem
                {
                    GameId = item.Id,
                    Quantity = item.Quantity
                }).ToList();
            }

            var authResult = await authorizationService.AuthorizeAsync(
                user,
                basket,
                new OwnerOrAdminRequirement()
            );

            if (!authResult.Succeeded) return Results.Forbid();

            await dbContext.SaveChangesAsync();

            return Results.NoContent();
        });
    }
}