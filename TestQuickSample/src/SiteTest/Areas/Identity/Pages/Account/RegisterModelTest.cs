using System.Text.Json;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Site.Areas.Identity.Pages.Account;
using SiteTest.TestingTools.Mocks;

namespace SiteTest.Areas.Identity.Pages.Account;

public class RegisterModelTest
{
    [Theory]
    [InlineData(null)]
    public async Task OnPostAsync_WhenModelStateIsNotValid_ShouldReturnPage(string? returnUrl = null)
    {
        var userStore = Substitute.For<IUserStore<IdentityUser>>();
        // var signInManager = Substitute.For<SignInManager<IdentityUser>>(userStore);
        var userManager = Substitute.For<UserManager<IdentityUser>>(userStore, null, null, null, null, null, null, null, null);
        var emailStore = Substitute.For<IUserEmailStore<IdentityUser>>(userStore);
        var logger = Substitute.For<ILogger<RegisterModel>>();
        var emailSender = Substitute.For<IEmailSender>();

        RegisterModel registerModel = new(
            userManager: userManager,
            userStore: userStore,
            signInManager: null,
            logger: logger,
            emailSender: emailSender
        );

        registerModel.BindViewModel(new RegisterModel.InputModel());

        var result = await registerModel.OnPostAsync(returnUrl);

        Assert.False(registerModel.ModelState.IsValid);



        Assert.True(true);
    }
}