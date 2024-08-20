using System;
using WebApplication1.Dtos;
using WebApplication1.DTOs;
using WebApplication1.Entities;

namespace WebApplication1.Mappers;

public static class GameMapping
{
    public static Game toEntity(this CreateGameDto game){
        return new(){
                Name = game.Name,
                GenreId = game.GenreId,
                Price = game.Price,
                ReleaseDate = game.ReleaseDate
        };
    }

    public static GameDto toDto(this Game game){
        return new(
            game.Id,
            game.Name,
            game.Genre!.Name,
            game.Price,
            game.ReleaseDate
            );
    }

    public static GameDetailDto toDetailDto(this Game game){
        return new(
            game.Id,
            game.Name,
            game.GenreId,
            game.Price,
            game.ReleaseDate
        );
    }

    public static List<GameDetailDto> toDetailDto(this List<Game> games){
        List<GameDetailDto> gameDetailDtos = [];

        games.ForEach(game => gameDetailDtos.Add(game.toDetailDto()));

        return gameDetailDtos;
    }
}
