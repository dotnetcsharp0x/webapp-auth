using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using webapp_auth.Models;

namespace webapp_auth.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PersonController : ControllerBase {

    [HttpPost]
    [Route("test")]
    public async Task<Person> Test(string Email, string Password) 
    {
    // находим пользователя 
    //Person? person = people.FirstOrDefault(p => p.Email == email && p.Password == password);
    Person person = new Person(Email,Password);
    // если пользователь не найден, отправляем статусный код 401

    
    // установка аутентификационных куки
    return person;
    }
   
}