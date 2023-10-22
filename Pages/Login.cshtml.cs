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
            request.AddHeader("Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiRG1pdHJpeSIsImxldmVsIjoiMTIzIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiQWRtaW4iLCJuYmYiOjE2OTczODU3ODgsImV4cCI6MTY5NzM4NjM4OCwiaXNzIjoiUnViaSIsImF1ZCI6InJ1YmkuZGV2In0.S_E8mDbKsmLd0WoCY4-Msi57QdLorwD4bPQHxgS8j_0");
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
