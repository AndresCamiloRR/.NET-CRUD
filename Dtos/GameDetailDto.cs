using System;

namespace WebApplication1.Dtos;

public record class GameDetailDto(
    int Id, 
    string Name, 
    int Genre, 
    decimal Price, 
    DateOnly releaseDate
);
