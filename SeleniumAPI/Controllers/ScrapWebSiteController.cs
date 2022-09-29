using Microsoft.AspNetCore.Mvc;
using OpenQA.Selenium.Chrome;

namespace SeleniumAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class ScrapWebSiteController : ControllerBase
{
    [HttpGet]
    public async Task Get()
    {
        var driver = new ChromeDriver();

        driver.Navigate().GoToUrl("https://selenium.dev");

        driver.Quit();
    }
}
