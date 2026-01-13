using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using P1WebAppRazor.Interfaces;

namespace P1WebAppRazor.Services;

public class TokenService : ITokenService
{

    private readonly string secretKey;

    public TokenService(IConfiguration configuration)
    {
        secretKey = configuration["Jwt:SecretKey"] ?? throw new ArgumentNullException("secret key not provided !");
    }

    public string CreateToken(Guid userId, string email, string username, int expiryTime)
    {
        //   secret Key
        var key = Encoding.ASCII.GetBytes(secretKey); // ascii conversion 

        var handler = new JwtSecurityTokenHandler();


        var payload = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(

            [
                new Claim(ClaimTypes.NameIdentifier  , userId.ToString() ),
                new Claim(ClaimTypes.Email , email),
                new Claim(ClaimTypes.Name , username),
            ]),

            Expires = DateTime.UtcNow.AddDays(expiryTime) , 

            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key) , SecurityAlgorithms.HmacSha256)

        };


        var token = handler.CreateToken(payload);

        return handler.WriteToken(token);


    }

    public bool VerifyToken(string token)
    {
        throw new NotImplementedException();
    }

    public Guid VerifyTokenAndGetId(string token)
    {
        throw new NotImplementedException();
    }
}
