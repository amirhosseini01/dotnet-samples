using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Site.Models;

public class Book
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [StringLength(500)]
    public string UserId { get; set; } = string.Empty;
    
    [Required]
    [StringLength(250)]
    public string Title { get; set; } = string.Empty;
    
    public DateTime ReleaseDate { get; set; }

    [ForeignKey(nameof(UserId))]
    public virtual IdentityUser User { get; set; } = null!;
}