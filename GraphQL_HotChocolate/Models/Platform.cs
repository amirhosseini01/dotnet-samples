using System.ComponentModel.DataAnnotations;

namespace GraphQL_HotChocolate.Models;
    [GraphQLDescription("Represent any software that have commandLine")]
    public class Platform
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [GraphQLDescription("The Licence of the software")]
        public string LicenseKey { get; set; }
        public virtual ICollection<Command> Commands { get; set; }
    }