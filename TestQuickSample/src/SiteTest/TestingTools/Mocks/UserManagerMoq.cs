using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Moq;
using SiteTest.TestingTools.Constants;

namespace SiteTest.TestingTools.Mocks;

public static class UserManagerMoq
{
    public static Mock<UserManager<TUser>> MockUserManager<TUser>() where TUser : IdentityUser
    {
        List<IdentityUser> usersLs = new()
        {
            new IdentityUser() { Id = "1" },
            new IdentityUser() { Id = "2" }
        };
        var store = new Mock<IUserStore<TUser>>();
        var mgr = new Mock<UserManager<TUser>>(store.Object, null, null, null, null, null, null, null, null);
        mgr.Object.UserValidators.Add(new UserValidator<TUser>());
        mgr.Object.PasswordValidators.Add(new PasswordValidator<TUser>());

        mgr.Setup(x => x.DeleteAsync(It.IsAny<TUser>())).ReturnsAsync(IdentityResult.Success);
        mgr.Setup(x => x.CreateAsync(It.IsAny<TUser>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success).Callback<TUser, string>((x, y) => usersLs.Add(x));
        mgr.Setup(x => x.UpdateAsync(It.IsAny<TUser>())).ReturnsAsync(IdentityResult.Success);
        mgr.Setup(x => x.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync((TUser)new IdentityUser() { Id = CustomIdentityConstants.UserId });

        return mgr;
    }
}