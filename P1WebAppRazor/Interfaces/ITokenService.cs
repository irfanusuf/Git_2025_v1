using System;

namespace P1WebAppRazor.Interfaces;

public interface ITokenService
{

    public string CreateToken(Guid userId, string email, string username, int expiryTime);
    public bool VerifyToken(string token);
    public Guid VerifyTokenAndGetId(string token);


}
