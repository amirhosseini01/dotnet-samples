using EfCacheSample.Models;
using Microsoft.EntityFrameworkCore;

namespace EfCacheSample.Data;

public class MainContext: DbContext
{
    public MainContext(DbContextOptions<MainContext> options) : base(options)
    {}

    public DbSet<Person> Persons { get; set; }= null!;
    public DbSet<PersonAddress> PersonAddresses { get; set; }= null!;
}