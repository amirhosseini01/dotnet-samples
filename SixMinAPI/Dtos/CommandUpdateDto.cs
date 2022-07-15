using System.ComponentModel.DataAnnotations;

namespace SixMiniAPI.Dtos;

public class CommandUpdateDto
{
    [Required]
    public string? HowTo { get; set; }

    [Required]
    [MaxLength(5)]
    public string? Platform { get; set; }

    [Required]
    public string? CommandLine { get; set; }
}