using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using RepoDb;

namespace RepoDbSample.Controllers;

[ApiController]
[Route("[controller]")]
public class CrudController : ControllerBase
{
    private readonly AppSettings _appSettings;
    public CrudController(IConfiguration configuration)
    {
        _appSettings = configuration.Get<AppSettings>();
    }

    [HttpGet(nameof(InsertOne))]
    public object InsertOne()
    {
        var person = new Person
        {
            Name = "John Doe",
            Age = 54,
            CreatedDateUtc = DateTime.UtcNow
        };
        using var connection = new SqlConnection(_appSettings.ConnectionString);
        var id = connection.Insert(person);
        return id;
    }

    [HttpGet(nameof(GetAll))]
    public IEnumerable<Person> GetAll()
    {
        using var connection = new SqlConnection(_appSettings.ConnectionString);
        var people = connection.QueryAll<Person>();
        return people;
    }

    [HttpGet($"{nameof(GetById)}/{nameof(id)}")]
    public object GetById(int id)
    {
        using var connection = new SqlConnection(_appSettings.ConnectionString);
        var person = connection.Query<Person>(e => e.Id == id);
        return person;
    }

    [HttpGet(nameof(InsertMany))]
    public object InsertMany()
    {
        var person = new List<Person>
        {
            new()
            {
                Name = "John Doe 2",
                Age = 54,
                CreatedDateUtc = DateTime.UtcNow
            },
            new()
            {
                Name = "John Doe 3",
                Age = 54,
                CreatedDateUtc = DateTime.UtcNow
            }
        };
        using var connection = new SqlConnection(_appSettings.ConnectionString);
        var rowsInserted = connection.InsertAll(person);

        return rowsInserted;
    }

    [HttpGet(nameof(Upsert))]
    public object Upsert()
    {
        var person = new Person
        {
            Name = "John Doe",
            Age = 54,
            CreatedDateUtc = DateTime.UtcNow
        };
        using var connection = new SqlConnection(_appSettings.ConnectionString);
        var id = connection.Merge(person);
        return id;
    }
}
