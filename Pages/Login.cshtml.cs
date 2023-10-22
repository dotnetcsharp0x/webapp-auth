using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using webapp_auth.Models;
using RestSharp;
using Nancy.Json;

namespace webapp_auth.Pages;

public class LoginModel : PageModel
{
    private readonly ILogger<LoginModel> _logger;

    public LoginModel(ILogger<LoginModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
        if( HttpContext.User.Claims.FirstOrDefault() != null){
            Console.WriteLine(HttpContext.User.Claims.FirstOrDefault().Value);
        }
    }
    public void OnPost(Person person)
    {
        
        var options = new RestClientOptions("http://localhost:5141")
{
  MaxTimeout = -1,
};
            var client = new RestClient(options);
            var request = new RestSharp.RestRequest("/api/Person/test?Email="+person.Email+"&Password="+person.Password, Method.Post);
            var response = client.Execute(request).Content;
            Console.WriteLine(response);
            Console.WriteLine(response);
            JavaScriptSerializer? js1 = new JavaScriptSerializer();
            var users = js1.Deserialize<Person>(response);
            Console.WriteLine(users);
            Authenticate(users.Email);
            
    }
    private async Task Authenticate(string userName)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
}
