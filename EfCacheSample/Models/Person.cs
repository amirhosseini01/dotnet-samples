using System.ComponentModel.DataAnnotations;

namespace EfCacheSample.Models;

public class Person
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(250)]
    public string FirstName { get; set; } = null!;
    
    [Required]
    [StringLength(250)]
    public string LastName { get; set; } = null!;

    public virtual ICollection<PersonAddress> PersonAddresses { get; set; }= null!;
}