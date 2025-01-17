﻿using System.Globalization;
using System.Net.Http.Headers;
using GameStore.Frontend.Models;

namespace GameStore.Frontend.Clients;

public class GamesClient(HttpClient httpClient)
{
    public async Task<GamesPage> GetGamesAsync(int pageNumber, int pageSize, string? nameSearch)
    {
        var query = QueryString.Create("pageNumber", pageNumber.ToString())
            .Add("pageSize", pageSize.ToString());

        if (!string.IsNullOrWhiteSpace(nameSearch)) query = query.Add("name", nameSearch);

        return await httpClient.GetFromJsonAsync<GamesPage>($"games{query}")
               ?? new GamesPage(0, []);
    }

    public async Task<CommandResult> AddGameAsync(GameDetails game)
    {
        var response = await httpClient.PostAsync("games", ToMultiPartFormDataContent(game));
        return await response.HandleAsync();
    }

    public async Task<GameDetails> GetGameAsync(Guid id)
    {
        return await httpClient.GetFromJsonAsync<GameDetails>($"games/{id}")
               ?? throw new Exception("Could not find game!");
    }

    public async Task<CommandResult> UpdateGameAsync(GameDetails updatedGame)
    {
        var response = await httpClient.PutAsync($"games/{updatedGame.Id}", ToMultiPartFormDataContent(updatedGame));
        return await response.HandleAsync();
    }

    public async Task<CommandResult> DeleteGameAsync(Guid id)
    {
        var response = await httpClient.DeleteAsync($"games/{id}");
        return await response.HandleAsync();
    }

    private static MultipartFormDataContent ToMultiPartFormDataContent(GameDetails game)
    {
        var formData = new MultipartFormDataContent
        {
            { new StringContent(game.Name), nameof(game.Name) },
            { new StringContent(game.GenreId.ToString()!), nameof(game.GenreId) },
            { new StringContent(game.Price.ToString(CultureInfo.InvariantCulture)), nameof(game.Price) },
            { new StringContent(game.ReleaseDate.ToString("yyyy-MM-dd")), nameof(game.ReleaseDate) },
            { new StringContent(game.Description), nameof(game.Description) }
        };

        if (game.ImageFile is null) return formData;

        var streamContent = new StreamContent(game.ImageFile.OpenReadStream())
        {
            Headers = { ContentType = new MediaTypeHeaderValue(game.ImageFile.ContentType) }
        };

        formData.Add(streamContent, "ImageFile", game.ImageFile.FileName);

        return formData;
    }
}