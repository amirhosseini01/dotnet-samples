using System.ComponentModel.DataAnnotations;

namespace Site.ViewModels.Books;

public class VmBookInput
{
    public int Id { get; set; }
    
    [Required]
    [StringLength(450)]
    public string UserId { get; set; } = string.Empty;
    
    [Required]
    [StringLength(250)]
    public string Title { get; set; } = string.Empty;
    
    public DateTime ReleaseDate { get; set; }
}