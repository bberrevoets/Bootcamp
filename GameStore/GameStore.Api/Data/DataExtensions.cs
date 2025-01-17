using GameStore.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Data;

public static class DataExtensions
{
    public static async Task InitializeDbAsync(this WebApplication app)
    {
        await app.MigrateDbAsync();
        await app.SeedDbAsync();

        app.Logger.LogInformation(18, "The database is ready!");
    }

    private static async Task MigrateDbAsync(this WebApplication app)
    {
        if (!app.Environment.IsDevelopment()) return;

        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<GameStoreContext>();
        await context.Database.MigrateAsync();
    }

    private static async Task SeedDbAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<GameStoreContext>();

        if (await dbContext.Genres.AnyAsync()) return;

        dbContext.Genres.AddRange(
            new Genre { Id = new Guid("3a2f2abc-3a4a-4e1a-b838-8c52590f67e5"), Name = "Fighting" },
            new Genre { Id = new Guid("5299de81-95fe-47b9-8db1-05f4ad52f698"), Name = "Kids and Family" },
            new Genre { Id = new Guid("dcbc0d6e-4e5f-4b92-b950-eeed7a185d7e"), Name = "Racing" },
            new Genre { Id = new Guid("62998639-ff87-4399-85d1-942bfde92e02"), Name = "Roleplaying" },
            new Genre { Id = new Guid("20ee5594-ba79-4a54-922b-ddb59c233aca"), Name = "Sports" }
        );
        await dbContext.SaveChangesAsync();

        await dbContext.Games.AddRangeAsync(
            new Game
            {
                Id = new Guid("19ed119b-4e7c-4f13-906a-adc4ba3bf1c7"),
                Name = "Final Fantasy XIV",
                Genre = await dbContext.Genres.FindAsync(new Guid("62998639-ff87-4399-85d1-942bfde92e02")),
                GenreId = new Guid("62998639-ff87-4399-85d1-942bfde92e02"),
                Price = 59.99m,
                ReleaseDate = new DateOnly(2010, 09, 30),
                Description =
                    "Join over 27 million adventurers worldwide and take part in an epic and ever-changing FINAL FANTASY. Experience an unforgettable story, exhilarating battles, and a myriad of captivating environments to explore.",
                ImageUri = "https://placehold.co/100",
                LastUpdatedBy = "bberr"
            },
            new Game
            {
                Id = new Guid("F9F676A0-9F44-49C4-9492-323CC44B4112"),
                Name = "Minecraft",
                Genre = await dbContext.Genres.FindAsync(new Guid("5299DE81-95FE-47B9-8DB1-05F4AD52F698")),
                GenreId = new Guid("5299DE81-95FE-47B9-8DB1-05F4AD52F698"),
                Price = 19.99m,
                ReleaseDate = new DateOnly(2011, 11, 18),
                Description =
                    "Survive the night or create a work of art in the hit sandbox game! Build anything you can imagine, uncover mysteries, and face thrilling foes in an infinite world that's unique in every playthrough. There's no wrong way to play!",
                ImageUri = "https://placehold.co/100",
                LastUpdatedBy = "bberr"
            },
            new Game
            {
                Id = new Guid("be3decbd-0592-4fb4-ae9c-90f531a4b1fd"),
                Name = "FIFA 23",
                Genre = await dbContext.Genres.FindAsync(new Guid("20ee5594-ba79-4a54-922b-ddb59c233aca")),
                GenreId = new Guid("20ee5594-ba79-4a54-922b-ddb59c233aca"),
                Price = 69.99m,
                ReleaseDate = new DateOnly(2022, 09, 27),
                Description =
                    "FIFA 23 brings The World's Game to the pitch, with HyperMotion2 Technology, men's and women's FIFA World Cup\u2122, women's club teams, cross-play features, and more.",
                ImageUri = "https://placehold.co/100",
                LastUpdatedBy = "bberr"
            },
            new Game
            {
                Id = new Guid("beb11f42-ca86-40a2-928e-35ed3ba42e68"),
                Name = "Street Fighter II",
                Genre = await dbContext.Genres.FindAsync(new Guid("3a2f2abc-3a4a-4e1a-b838-8c52590f67e5")),
                GenreId = new Guid("3a2f2abc-3a4a-4e1a-b838-8c52590f67e5"),
                Price = 19.99m,
                ReleaseDate = new DateOnly(1992, 07, 15),
                Description =
                    "Street Fighter 2, the most iconic fighting game of all time, is back on the Nintendo Switch! The newest iteration of SFII in nearly 10 years, Ultra Street Fighter 2 features all of the classic characters, a host of new single player and multiplayer features, as well as two new fighters: Evil Ryu and Violent Ken!",
                ImageUri = "https://placehold.co/100",
                LastUpdatedBy = "bberr"
            }
        );

        await dbContext.SaveChangesAsync();
    }
}