using Vives.Presentation.Authentication;

namespace PeopleManager.Ui.Mvc.Stores
{
    public class BearerTokenStore(IHttpContextAccessor httpContextAccessor) : IBearerTokenStore
    {
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        private const string CookieName = "BearerToken";


        public string GetToken()
        {
            if (httpContextAccessor.HttpContext is not null &&
                httpContextAccessor.HttpContext.Request.Cookies.ContainsKey(CookieName))
            {
                return httpContextAccessor.HttpContext.Request.Cookies[CookieName] ?? string.Empty;
            }

            return string.Empty;
        }

        public void SetToken(string token)
        {
            if (httpContextAccessor.HttpContext is null)
            {
                return;
            }

            if (httpContextAccessor.HttpContext.Request.Cookies.ContainsKey(CookieName))
            {
                httpContextAccessor.HttpContext.Response.Cookies.Delete(CookieName);
            }

            httpContextAccessor.HttpContext.Response.Cookies.Append(CookieName, token, new CookieOptions { HttpOnly = true });
        }
    }
}
