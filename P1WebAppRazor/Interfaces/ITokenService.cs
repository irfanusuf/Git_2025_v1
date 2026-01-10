using System;

namespace P1WebAppRazor.Interfaces;

public interface ITokenService
{

 public string CreateToken(string userDetails , int expiryTime );
 public bool VerifyToken(string token);



 


}
