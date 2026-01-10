using System;
using P1WebAppRazor.Interfaces;

namespace P1WebAppRazor.Services;

public class TokenService : ITokenService 
{
    public string CreateToken(string userDetails, int expiryTime)
    {
        try
        {
            // implemntation 

        }
        catch (System.Exception)
        {

            throw;
        }


        throw new NotImplementedException();
    }

    public bool VerifyToken(string token)
    {
        throw new NotImplementedException();
    }

 
}
