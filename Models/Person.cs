namespace webapp_auth.Models;

public class Person {
    public Person (string _email, string _password) {
        Email = _email;
        Password = _password;
    }
    public Person() 
    {
        
    }
    public string Email {get;set;}
    public string Password{get;set;}
}