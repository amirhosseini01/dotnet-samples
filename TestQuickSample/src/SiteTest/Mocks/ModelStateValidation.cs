using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SiteTest.Mocks;

public static class ModelStateValidation
{
    public static void BindViewModel<T>(this PageModel razorPage, T model)
    {
        if (model == null) return;

        var context = new ValidationContext(model, null, null);
        var results = new List<ValidationResult>();

        if (!Validator.TryValidateObject(model, context, results, true))
        {
            razorPage.ModelState.Clear();
            foreach (ValidationResult result in results)
            {
                var key = result.MemberNames.FirstOrDefault() ?? string.Empty;
                razorPage.ModelState.AddModelError(key, result.ErrorMessage?? string.Empty);
            }
        }
    }
}