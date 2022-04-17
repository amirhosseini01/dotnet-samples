using System.ComponentModel.DataAnnotations;

namespace GraphQL_HotChocolate.Models;
    public class Command
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int PlatformId { get; set; }
        [Required]
        public string HowTo { get; set; }
        [Required]
        public string CommandLine { get; set; }
        public Platform Platform { get; set; }
    }