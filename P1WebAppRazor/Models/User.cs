using System;

namespace P1WebAppRazor.Models;

public class User
{
    public required Guid UserId { get; set; } = Guid.NewGuid();   // default value for userId 
    public required string Username { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }

    public string? Otp {get;set;}

    public ICollection<Blog> Blogs {get ;set;} = [];

}
