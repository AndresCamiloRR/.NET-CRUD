using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DTOs;

public record class CreateGameDto(
    [Required][StringLength(50)]string Name, 
    int GenreId, 
    [Required][Range(1, 100)]decimal Price, 
    [Required]DateOnly ReleaseDate);
