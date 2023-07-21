using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Site.Data;
using Site.Helpers.Identity;
using Site.Pages.Books;
using SiteTest.TestingTools.Mocks.DataBase;

namespace SiteTest.Pages.Books;

public class DetailTests : IClassFixture<TestDatabaseFixture>
{
    public TestDatabaseFixture Fixture { get; }
    public DetailTests(TestDatabaseFixture fixture) => Fixture = fixture;
    [Theory, MemberData(nameof(GetInvalidIntIds))]
    public async Task OnGetAsync_WhenIdIsNotValid_ReturnsNotFound(int? id)
    {
        // Arrange
        await using ApplicationDbContext context = Fixture.CreateContext();
        DetailsModel razorPage = new(context: context);

        // act
        NotFoundResult? handlerResult = (await razorPage.OnGetAsync(id)) as NotFoundResult;

        // assert
        NotFoundResult expectedResult = new NotFoundResult();;

        Assert.NotNull(handlerResult);
        Assert.Equal(expectedResult.StatusCode, handlerResult.StatusCode);
    }
    
    public static List<object[]> GetInvalidIntIds =>
        new()
        {
            new object[]
            {
                null
            },
            new object[]
            {
                -1
            },
            new object[]
            {
                0
            },
        };
}