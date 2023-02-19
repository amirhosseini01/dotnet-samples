using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace RepoDbSample.Controllers;

[ApiController]
[Route("[controller]")]
public class CrudController : ControllerBase
{
    public CrudController()
    {
    }

    // [HttpGet("CreatingRecord")]
    // public IEnumerable<WeatherForecast> Get()
    // {
    //     var person = new Person
    //     {
    //         Name = "John Doe",
    //         Age = 54,
    //         CreatedDateUtc = DateTime.UtcNow
    //     };
    //     using (var connection = new SqlConnection(ConnectionString))
    //     {
    //         var id = connection.Insert(person);
    //     }

    // }
}
