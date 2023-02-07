namespace FirstApp.CookieTools;

public static class AppendCookie
{
    public static void AppendSameSiteOldBrowser(HttpResponse response,
        string cookieKey, string cookieValue)
    {
        var cookieOptions = new CookieOptions
        {
            // Set the secure flag, which Chrome's changes will require for SameSite none.
            // Note this will also require you to be running on HTTPS.
            Secure = true,

            // Set the cookie to HTTP only which is good practice unless you really do need
            // to access it client side in scripts.
            HttpOnly = true,

            // Add the SameSite attribute, this will emit the attribute with a value of none.
            SameSite = SameSiteMode.None

            // The client should follow its default cookie policy.
            // SameSite = SameSiteMode.Unspecified
        };

        // Add the cookie to the response cookie collection
        response.Cookies.Append(cookieKey, cookieValue, cookieOptions);
    }

    public static void AppendSameSiteNewBrowser(HttpResponse response,
        string cookieKey, string cookieValue)
    {
        response.Cookies.Append(
                     cookieKey, cookieValue,
                     new CookieOptions() { SameSite = SameSiteMode.Lax });
    }
}