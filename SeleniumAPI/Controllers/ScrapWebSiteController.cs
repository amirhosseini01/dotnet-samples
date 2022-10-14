using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace SeleniumAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class ScrapWebSiteController : ControllerBase
{
    private const string _specialProductsCssSelector = ".d-block.pointer.pos-relative.bg-000.overflow-hidden.grow-1.py-3.px-4.px-2-lg.h-full-md.shrink-0.ml-1.ml-0-lg.px-3-xs.px-4-lg";
    [HttpGet(nameof(GetImplicitWait))]
    public long GetImplicitWait()
    {
        Stopwatch sw = new();
        sw.Start();

        using var driver = new ChromeDriver(chromeDriverDirectory:
            "C:\\Program Files\\Google\\Chrome\\Application");
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        driver.Url = "https://www.digikala.com/";

        IReadOnlyCollection<IWebElement> specialProducts =
            driver.FindElements(
                By.CssSelector(_specialProductsCssSelector))
            ;
        if (specialProducts is null || specialProducts.Count == 0)
            return -1;

        int deltaY = specialProducts!.First().Location.Y;
        new Actions(driver)
            .ScrollByAmount(0, deltaY)
            .Perform();

        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

        foreach (var item in specialProducts)
        {
            var test = item.FindElement(By.TagName("img")).GetAttribute("src");
        }

        sw.Stop();
        driver.Close();
        return sw.ElapsedMilliseconds;
    }
    [HttpGet(nameof(GetExplicitWait))]
    public long GetExplicitWait()
    {
        Stopwatch sw = new();
        sw.Start();

        using var driver = new ChromeDriver(chromeDriverDirectory:
            "C:\\Program Files\\Google\\Chrome\\Application");
        driver.Url = "https://www.digikala.com/";

        WebDriverWait wait = new(driver, TimeSpan.FromSeconds(10))
        {
            PollingInterval = TimeSpan.FromSeconds(10),
        };
          wait.IgnoreExceptionTypes(typeof(NoSuchElementException));

        IReadOnlyCollection<IWebElement> specialProducts = wait.Until(e => e.FindElements(By.CssSelector(_specialProductsCssSelector)));
        if (specialProducts is null || specialProducts.Count == 0)
            return -1;

        int deltaY = specialProducts!.First().Location.Y;
        new Actions(driver)
            .ScrollByAmount(0, deltaY)
            .Perform();

        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

        foreach (var item in specialProducts)
        {
            var test = item.FindElement(By.TagName("img")).GetAttribute("src");
        }

        sw.Stop();
        driver.Close();
        return sw.ElapsedMilliseconds;
    }
}
