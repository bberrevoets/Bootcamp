-- Script Date: 19-1-2025 21:42  - ErikEJ.SqlCeScripting version 3.5.2.95
-- Database information:
-- Database: E:\Repos\DotnetAcademy\Bootcamp\GameStore\GameStore.Api\GameStore.db
-- ServerVersion: 3.40.0
-- DatabaseSize: 68 KB
-- Created: 21-12-2024 11:10

-- User Table information:
-- Number of tables: 6
-- __EFMigrationsHistory: -1 row(s)
-- __EFMigrationsLock: -1 row(s)
-- BasketItems: -1 row(s)
-- Baskets: -1 row(s)
-- Games: -1 row(s)
-- Genres: -1 row(s)

SELECT 1;
PRAGMA foreign_keys=OFF;
BEGIN TRANSACTION;
CREATE TABLE [Genres] (
  [Id] text NOT NULL
, [Name] text NOT NULL
, CONSTRAINT [sqlite_autoindex_Genres_1] PRIMARY KEY ([Id])
);
CREATE TABLE [Games] (
  [Id] text NOT NULL
, [Name] text NOT NULL
, [GenreId] text NOT NULL
, [Price] text NOT NULL
, [ReleaseDate] text NOT NULL
, [Description] text NOT NULL
, [ImageUri] text DEFAULT ('') NOT NULL
, [LastUpdatedBy] text DEFAULT ('berre') NOT NULL
, CONSTRAINT [sqlite_autoindex_Games_1] PRIMARY KEY ([Id])
, CONSTRAINT [FK_Games_0_0] FOREIGN KEY ([GenreId]) REFERENCES [Genres] ([Id]) ON DELETE CASCADE ON UPDATE NO ACTION
);
CREATE TABLE [Baskets] (
  [Id] text NOT NULL
, CONSTRAINT [sqlite_autoindex_Baskets_1] PRIMARY KEY ([Id])
);
CREATE TABLE [BasketItems] (
  [Id] text NOT NULL
, [GameId] text NOT NULL
, [Quantity] bigint NOT NULL
, [CustomerBasketId] text NOT NULL
, CONSTRAINT [sqlite_autoindex_BasketItems_1] PRIMARY KEY ([Id])
, CONSTRAINT [FK_BasketItems_0_0] FOREIGN KEY ([GameId]) REFERENCES [Games] ([Id]) ON DELETE CASCADE ON UPDATE NO ACTION
, CONSTRAINT [FK_BasketItems_1_0] FOREIGN KEY ([CustomerBasketId]) REFERENCES [Baskets] ([Id]) ON DELETE CASCADE ON UPDATE NO ACTION
);
CREATE TABLE [__EFMigrationsLock] (
  [Id] bigint NOT NULL
, [Timestamp] text NOT NULL
, CONSTRAINT [sqlite_master_PK___EFMigrationsLock] PRIMARY KEY ([Id])
);
CREATE TABLE [__EFMigrationsHistory] (
  [MigrationId] text NOT NULL
, [ProductVersion] text NOT NULL
, CONSTRAINT [sqlite_autoindex___EFMigrationsHistory_1] PRIMARY KEY ([MigrationId])
);
INSERT INTO [Genres] ([Id],[Name]) VALUES (
'20EE5594-BA79-4A54-922B-DDB59C233ACA','Sports');
INSERT INTO [Genres] ([Id],[Name]) VALUES (
'3A2F2ABC-3A4A-4E1A-B838-8C52590F67E5','Fighting');
INSERT INTO [Genres] ([Id],[Name]) VALUES (
'5299DE81-95FE-47B9-8DB1-05F4AD52F698','Kids and Family');
INSERT INTO [Genres] ([Id],[Name]) VALUES (
'62998639-FF87-4399-85D1-942BFDE92E02','Roleplaying');
INSERT INTO [Genres] ([Id],[Name]) VALUES (
'DCBC0D6E-4E5F-4B92-B950-EEED7A185D7E','Racing');
INSERT INTO [Games] ([Id],[Name],[GenreId],[Price],[ReleaseDate],[Description],[ImageUri],[LastUpdatedBy]) VALUES (
'19ED119B-4E7C-4F13-906A-ADC4BA3BF1C7','Final Fantasy XIV','62998639-FF87-4399-85D1-942BFDE92E02','59.99','2010-09-30','Join over 27 million adventurers worldwide and take part in an epic and ever-changing FINAL FANTASY. Experience an unforgettable story, exhilarating battles, and a myriad of captivating environments to explore.','http://localhost:5082/GameImages/FinalFantasyXIV.png','bberr');
INSERT INTO [Games] ([Id],[Name],[GenreId],[Price],[ReleaseDate],[Description],[ImageUri],[LastUpdatedBy]) VALUES (
'377096DA-1A78-4522-964E-62B1D7A475AE','ASTRO BOT','62998639-FF87-4399-85D1-942BFDE92E02','59.97','2024-09-06','Dive into a breathtaking intergalactic adventure with Astro, the lovable robot hero, as you explore dazzling worlds, conquer clever challenges, and experience the innovative magic of the PS5 like never before!','http://localhost:5082/GameImages/AstroBot.jpg','bberr');
INSERT INTO [Games] ([Id],[Name],[GenreId],[Price],[ReleaseDate],[Description],[ImageUri],[LastUpdatedBy]) VALUES (
'48DAFD78-1730-4178-970D-4225AB8067A3','Super Mario Bros 3','5299DE81-95FE-47B9-8DB1-05F4AD52F698','19.99','1988-10-13','Control the jumping plumbers Mario and Luigi as they embark on a quest to save Princess ToadStool.','http://localhost:5082/GameImages/nes_supermariobros3.jpg','bberr');
INSERT INTO [Games] ([Id],[Name],[GenreId],[Price],[ReleaseDate],[Description],[ImageUri],[LastUpdatedBy]) VALUES (
'4F1D5F2D-E641-4219-A507-706EF215EEF7','Black Myth: Wukung','62998639-FF87-4399-85D1-942BFDE92E02','59.99','2024-08-20','Assume the role of the Destined One, a monkey warrior, as you traverse breathtaking landscapes, engage in dynamic combat, and uncover the hidden truths behind ancient legends.','http://localhost:5082/GameImages/BlackMythWukong.jpg','bberr');
INSERT INTO [Games] ([Id],[Name],[GenreId],[Price],[ReleaseDate],[Description],[ImageUri],[LastUpdatedBy]) VALUES (
'B2743B6A-576D-4C56-AAE5-9F56DCD48A81','The Lengend of Zelda: Tears of the Kingdom','62998639-FF87-4399-85D1-942BFDE92E02','69.99','2023-05-12','Venture into the sprawling, awe-inspiring world of The Legend of Zelda:Tear of the Kingdom, where daring exploration, intricate puzzles, and epic battles shape the next chapter of Link''s legendary journey to save Hyrule.','http://localhost:5082/GameImages/5c834770-d564-4e72-896c-4bc911b152ba.jpg','alice@gamestore.com');
INSERT INTO [Games] ([Id],[Name],[GenreId],[Price],[ReleaseDate],[Description],[ImageUri],[LastUpdatedBy]) VALUES (
'BE3DECBD-0592-4FB4-AE9C-90F531A4B1FD','FIFA 23','20EE5594-BA79-4A54-922B-DDB59C233ACA','69.99','2022-09-27','FIFA 23 brings The World''s Game to the pitch, with HyperMotion2 Technology, men''s and women''s FIFA World Cup™, women''s club teams, cross-play features, and more.','http://localhost:5082/GameImages/FIFA23.jpg','bberr');
INSERT INTO [Games] ([Id],[Name],[GenreId],[Price],[ReleaseDate],[Description],[ImageUri],[LastUpdatedBy]) VALUES (
'BEB11F42-CA86-40A2-928E-35ED3BA42E68','Street Fighter II','3A2F2ABC-3A4A-4E1A-B838-8C52590F67E5','19.99','1992-07-15','Street Fighter 2, the most iconic fighting game of all time, is back on the Nintendo Switch! The newest iteration of SFII in nearly 10 years, Ultra Street Fighter 2 features all of the classic characters, a host of new single player and multiplayer features, as well as two new fighters: Evil Ryu and Violent Ken!','http://localhost:5082/GameImages/StreetFighterII.jpg','bberr');
INSERT INTO [Games] ([Id],[Name],[GenreId],[Price],[ReleaseDate],[Description],[ImageUri],[LastUpdatedBy]) VALUES (
'F9F676A0-9F44-49C4-9492-323CC44B4112','Minecraft','5299DE81-95FE-47B9-8DB1-05F4AD52F698','19.99','2011-11-18','Survive the night or create a work of art in the hit sandbox game! Build anything you can imagine, uncover mysteries, and face thrilling foes in an infinite world that''s unique in every playthrough. There''s no wrong way to play!','http://localhost:5082/GameImages/Minecraft.jpg','bberr');
INSERT INTO [Baskets] ([Id]) VALUES (
'FDBCF092-2108-4200-8CA5-9B393B5F1D66');
INSERT INTO [BasketItems] ([Id],[GameId],[Quantity],[CustomerBasketId]) VALUES (
'1EFEE15E-22CB-48DD-8C92-9072D836012A','BE3DECBD-0592-4FB4-AE9C-90F531A4B1FD',2,'FDBCF092-2108-4200-8CA5-9B393B5F1D66');
INSERT INTO [__EFMigrationsHistory] ([MigrationId],[ProductVersion]) VALUES (
'20241118151113_InitialCreate','9.0.0');
INSERT INTO [__EFMigrationsHistory] ([MigrationId],[ProductVersion]) VALUES (
'20241221101033_addFileUri','9.0.0');
INSERT INTO [__EFMigrationsHistory] ([MigrationId],[ProductVersion]) VALUES (
'20250117104252_AddLastUpdatedBy','8.0.12');
INSERT INTO [__EFMigrationsHistory] ([MigrationId],[ProductVersion]) VALUES (
'20250117123435_AddCustomerBasket','8.0.12');
CREATE INDEX [Games_IX_Games_GenreId] ON [Games] ([GenreId] ASC);
CREATE INDEX [BasketItems_IX_BasketItems_GameId] ON [BasketItems] ([GameId] ASC);
CREATE INDEX [BasketItems_IX_BasketItems_CustomerBasketId] ON [BasketItems] ([CustomerBasketId] ASC);
CREATE TRIGGER [fki_Games_GenreId_Genres_Id] BEFORE Insert ON [Games] FOR EACH ROW BEGIN SELECT RAISE(ROLLBACK, 'Insert on table Games violates foreign key constraint FK_Games_0_0') WHERE (SELECT Id FROM Genres WHERE  Id = NEW.GenreId) IS NULL; END;
CREATE TRIGGER [fku_Games_GenreId_Genres_Id] BEFORE Update ON [Games] FOR EACH ROW BEGIN SELECT RAISE(ROLLBACK, 'Update on table Games violates foreign key constraint FK_Games_0_0') WHERE (SELECT Id FROM Genres WHERE  Id = NEW.GenreId) IS NULL; END;
CREATE TRIGGER [fki_BasketItems_GameId_Games_Id] BEFORE Insert ON [BasketItems] FOR EACH ROW BEGIN SELECT RAISE(ROLLBACK, 'Insert on table BasketItems violates foreign key constraint FK_BasketItems_0_0') WHERE (SELECT Id FROM Games WHERE  Id = NEW.GameId) IS NULL; END;
CREATE TRIGGER [fku_BasketItems_GameId_Games_Id] BEFORE Update ON [BasketItems] FOR EACH ROW BEGIN SELECT RAISE(ROLLBACK, 'Update on table BasketItems violates foreign key constraint FK_BasketItems_0_0') WHERE (SELECT Id FROM Games WHERE  Id = NEW.GameId) IS NULL; END;
CREATE TRIGGER [fki_BasketItems_CustomerBasketId_Baskets_Id] BEFORE Insert ON [BasketItems] FOR EACH ROW BEGIN SELECT RAISE(ROLLBACK, 'Insert on table BasketItems violates foreign key constraint FK_BasketItems_1_0') WHERE (SELECT Id FROM Baskets WHERE  Id = NEW.CustomerBasketId) IS NULL; END;
CREATE TRIGGER [fku_BasketItems_CustomerBasketId_Baskets_Id] BEFORE Update ON [BasketItems] FOR EACH ROW BEGIN SELECT RAISE(ROLLBACK, 'Update on table BasketItems violates foreign key constraint FK_BasketItems_1_0') WHERE (SELECT Id FROM Baskets WHERE  Id = NEW.CustomerBasketId) IS NULL; END;
COMMIT;

