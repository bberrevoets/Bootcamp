using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using GameStore.Frontend.Models;

namespace GameStore.Frontend.Clients;

public class GenresClient(HttpClient httpClient)
{
    public async Task<Genre[]> GetGenresAsync() 
        => await httpClient.GetFromJsonAsync<Genre[]>("genres") ?? [];
}
