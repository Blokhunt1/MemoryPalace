
using Microsoft.AspNetCore.Authentication;
using Auth0.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MemoryPalaceApp.Pages
{
    public class LoginModel : PageModel
    {
        public async Task OnGet(string redirectUri = "/")
        {
            var returnUrl = string.IsNullOrEmpty(redirectUri) ? "/" : redirectUri;
            
            var authenticationProperties = new LoginAuthenticationPropertiesBuilder()
                .WithRedirectUri(returnUrl)
                .Build();

            // Use HTTPS since NGINX handles SSL termination
            var scheme = HttpContext.Request.Headers["X-Forwarded-Proto"].FirstOrDefault() ?? "https";
            authenticationProperties.RedirectUri = $"{scheme}://{HttpContext.Request.Host}/callback";

            await HttpContext.ChallengeAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
        }
    }
}
