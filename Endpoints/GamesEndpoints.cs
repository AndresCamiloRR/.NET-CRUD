using System;
using WebApplication1.Data;
using WebApplication1.DTOs;
using WebApplication1.Entities;
using WebApplication1.Mappers;

namespace WebApplication1.Endpoints;

public static class GamesEndpoints
{
    const string GetGameEndpointName = "GetGame";

    // Example list of games (for illustration purposes)
    private static readonly List<GameDto> games = new List<GameDto> 
    {
        new GameDto(1, "Crash Bandicoot", "Adventure", 100.00M, new DateOnly(2022, 9, 27)),
        new GameDto(2, "Sonic Heroes", "Adventure", 100.00M, new DateOnly(2024, 9, 27))
    };

    public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("games").WithParameterValidation();

        // GET: Retrieve all games
        group.MapGet("/", (GameStoreContext dbContext) => dbContext.Games.ToList().toDetailDto());

        // GET: Retrieve a game by ID
        group.MapGet("/{id}", (int id, GameStoreContext dbContext) => 
        {
            Game? game = dbContext.Games.Find(id);
            return game is null ? Results.NotFound() : Results.Ok(game.toDetailDto());
        })
        .WithName(GetGameEndpointName);

        // POST: Create a new game
        group.MapPost("/", (CreateGameDto newGame, GameStoreContext dbContext) => 
        {
            Game game = newGame.toEntity();
            game.Genre = dbContext.Genre.Find(newGame.GenreId);

            dbContext.Add(game);
            dbContext.SaveChanges();

            return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id}, game.toDto());
        });

        // PUT: Update an existing game by ID
        group.MapPut("/{id}", (int id, CreateGameDto newGame, GameStoreContext dbContext) => 
        {
            Game? toUpdate = dbContext.Games.Find(id);
            
            if (toUpdate is null)
            {
                return Results.NotFound();
            }

            toUpdate.Name = newGame.Name;
            toUpdate.Genre = dbContext.Genre.Find(newGame.GenreId);
            toUpdate.Price = newGame.Price;
            toUpdate.ReleaseDate = newGame.ReleaseDate;
            
            dbContext.SaveChanges();

            return Results.NoContent();
        });

        // DELETE: Remove a game by ID
        group.MapDelete("/{id}", (int id, GameStoreContext dbContext) => 
        {
            Game? game = dbContext.Games.Find(id);

            if (game is not null)
            {
                dbContext.Games.Remove(game);
                dbContext.SaveChanges();
            }

            return Results.NoContent();
        });

        return group;
    }
}
