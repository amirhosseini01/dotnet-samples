using System.ComponentModel.DataAnnotations;

namespace CommandsService.Dto;
public class CommandDto
{
}
public class CommandReadDto
{
    public int Id { get; set; }
    public string HowTo { get; set; }
    public string CommandLine { get; set; }
    public int PlatformId { get; set; }
}
public class CommandCreateDto
{
    [Required]
    public string HowTo { get; set; }

    [Required]
    public string CommandLine { get; set; }
}