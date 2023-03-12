using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EfCacheSample.Models;

public class PersonAddress
{
    [Key]
    public int Id { get; set; }

    public int PersonId { get; set; }

     [Required]
    [StringLength(500)]
    public string Address { get; set; }= null!;

    [ForeignKey(nameof(PersonId))]
    public virtual Person Person { get; set; }= null!;
}