using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Site.Data;
using Site.Helpers.Identity;
using Site.Pages.Books;
using Site.ViewModels.Books;
using SiteTest.TestingTools.Mocks;
using SiteTest.TestingTools.Mocks.DataBase;

namespace SiteTest.Pages.Books;

public class CreateTests : IClassFixture<TestDatabaseFixture>
{
    public TestDatabaseFixture Fixture { get; }
    public CreateTests(TestDatabaseFixture fixture) => Fixture = fixture;
    
    [Fact]
    public async Task OnPostAsync_WhenUserHasNoCreateAccessInPage_ReturnsAccessDenied()
    {
        // Arrange
        CreateModel razorPage = new(context: null,
            authorizationService: new MoqAuthorizationService(returnsSuccessAuthorizeResult: false));

        // act
        IActionResult handlerResult = await razorPage.OnPostAsync();

        // assert
        var expectedResult = AuthorizationHelper.ReturnForbiddenResult();

        Assert.Equal(JsonSerializer.Serialize(expectedResult), JsonSerializer.Serialize(handlerResult));
    }

    [Fact]
    public async Task OnPostAsync_WhenModelStateIsNotValidOrViewModelPropValuesIsIncorrect_ModelStateIsNotValid()
    {
        // Arrange
        CreateModel razorPage = new(context: null,
            authorizationService: new MoqAuthorizationService(returnsSuccessAuthorizeResult: true));
        VmBookInput input = new();

        razorPage.BindViewModel(input);

        // act
        IActionResult handlerResult = await razorPage.OnPostAsync();

        // assert
        Assert.False(razorPage.ModelState.IsValid);
    }

    [Fact]
    public async Task OnPostAsync_AddingEntity_ReturnsRedirectToPageResult()
    {
        // Arrange
        await using ApplicationDbContext context = Fixture.CreateContext();

        CreateModel razorPage = new(context: context,
            authorizationService: new MoqAuthorizationService(returnsSuccessAuthorizeResult: true))
        {
            PageContext = IdentityUserMock.MoqClaimsPrincipal()
        };

        VmBookInput input = new()
        {
            Title = "new test",
            ReleaseDate = DateTime.Now,
            UserId = context.Users.First().Id
        };
        razorPage.Book = input;

        // act
        var dbTransaction = await context.Database.BeginTransactionAsync();

        RedirectToPageResult handlerResult = (RedirectToPageResult)await razorPage.OnPostAsync();

        context.ChangeTracker.Clear();
        await dbTransaction.DisposeAsync();

        // assert
        var expectedResult = new RedirectToPageResult("./Index");
        
        Assert.Equal(expectedResult.PageName, handlerResult.PageName);
    }
}