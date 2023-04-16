using System.ComponentModel.DataAnnotations;
using Sieve.Attributes;

namespace SieveApiSample.Models;


public class Post
{
    [Key]
    public int Id { get; set; }

    [StringLength(250)]
    [Sieve(CanFilter = true, CanSort = true)]
    public string Title { get; set; }

    [Sieve(CanFilter = true, CanSort = true)]
    public int LikeCount { get; set; }

    [Sieve(CanFilter = true, CanSort = true)]
    public int CommentCount { get; set; }

    [Sieve(CanFilter = true, CanSort = true, Name = "created")]
    public DateTimeOffset DateCreated { get; set; } = DateTimeOffset.UtcNow;
}